' Input options from Event parameters
' Comma delimited values
' Param(0) = Vera Device ID
' Param(1) = Vera State Control ( 0 = Off, 1 = On)
' Works with all standard power devices.  For virtual switches change SwitchPower1 to VSwitchPower1
' Change Debug to False to disable debug logging

Sub Main(Optional ByVal sParam As String = " ")
    Dim Debug As Boolean = TRUE
    Dim logName As String = "Vera Device: "

    ' Update to use the URL:PORT to your Vera device
    ' Example:  http://192.168.0.10"
    ' Port: 3480 is default for all Vera boxes
    Const Server_url = "<URL VERA URL>"
    Const Server_port = "3480"

    Dim myData() As String = sParam.Split(",")
    Dim vDevice As Integer = CInt(myData(0))
    Dim vAction As String = CInt(myData(1))
    Dim Dev_url As String = "/data_request?id=action&DeviceNum=" & vDevice & "&serviceId=urn:upnp-org:serviceId:SwitchPower1&action=SetTarget&newTargetValue=" & vAction
    
    Dim Debug_string As String = Server_url & ":" & Server_port & Dev_url
    Dim Result As String = hs.GetURL(Server_url, Dev_url, TRUE, Server_port)

    If Debug 
        hs.Writelog(logName, vDevice & " " & vAction)
        hs.Writelog(logName, Debug_string)
        hs.Writelog(logName, Result)
    End If
End Sub