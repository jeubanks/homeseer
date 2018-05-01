Imports System.IO
Imports System.Net

Sub Main(parms As Object)
    Dim debug As Boolean = True                                 ' -- Change to False to disable debug logging
 	Dim logName As String = "HA-ChromeCast"			            ' -- Set log name for HS logging
    Dim Server_url As String = "<YOUR URL>"                     ' -- URL to Home Assistant Install
    Dim Server_port As String = "<HA-PORT>"                     ' -- HA Port
    dim mediaUrl As String = "<YOUR URL/PATH TO MP3"            ' -- URL to media
    Dim mp3File As String = "<MP3 FILENAME>"                    ' -- Filename of mp3/media to play
    Dim entityID As String = "<ENTITY_ID>"                      ' -- HA entity_id of media_player to send to

    Dim Headers As String = "Content-Type: application/json"
    Dim mediaApi As String = "/api/services/media_player/play_media"
    Dim playUrl As String = Server_url & ":" & Server_port & mediaApi
    Dim mediaContentId As String = mediaUrl & mp3File
    Dim mediaContentType As String = "audio/mp3"

    Dim myjsonData As String = String.Format("{{ ""media_content_id"": ""{0}"", ""media_content_type"": ""{1}"", ""entity_id"": ""{2}"" }}", mediaContentId, mediaContentType, entityID)

    Dim debugString1 As String = playUrl
    Dim debugString2 As String = myjsonData
    If debug Then
        hs.Writelog(logName, "Debug Player URL: " & debugString1)
        hs.Writelog(logName, "Debug JSON Data: " & debugString2)
    End If

    Dim myWebReq As HttpWebRequest
    Dim myWebResp As HttpWebResponse
    Dim encoding As New System.Text.UTF8Encoding
    Dim mySR As StreamReader

    Try
        Dim data As Byte() = encoding.GetBytes(myjsonData)
        myWebReq = DirectCast(WebRequest.Create(playUrl), HttpWebRequest)
        myWebReq.ContentType = "application/json; charset=utf-8"
        myWebReq.ContentLength = data.Length
        myWebReq.Method = "POST"
        Dim myStream As Stream = myWebReq.GetRequestStream()

        If data.Length > 0 Then
            myStream.Write(data, 0, data.Length)
            myStream.Close()
        End If

        myWebResp = DirectCast(myWebReq.GetResponse(), HttpWebResponse)
        mySR = New StreamReader(myWebResp.GetResponseStream())
        Dim responseText As String = mySR.ReadToEnd()

        If debug Then
            hs.Writelog(logName, "Response: " & responseText)
        End If

    Catch ex As Exception : hs.Writelog(logName, "Error: " & ex.Message.ToString)
    End Try
End Sub