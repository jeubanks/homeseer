Sub Main(parms As Object)
    Dim Debug As Boolean = True
    Const Server_url = "<YOUR VERA URL>"
    Const Server_port = "3480"
    
    '-- Change this to your device control string
    Dim Dev_url As String = "/data_request?id=action&DeviceNum=423&serviceId=urn:upnp-org:serviceId:SwitchPower1&action=SetTarget&newTargetValue=0"
    
    Dim Debug_string As String = Server_url &":" & Server_port & Dev_url
    Dim Result As String = hs.GetURL(Server_url, Dev_url, TRUE, Server_port)

    If Debug 
        hs.Writelog("Debug: ", Debug_string)
        hs.Writelog("Result: ", Result)
    End If
End Sub