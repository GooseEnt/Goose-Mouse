Public Class SerialPort
    Public SerialLog As New System.Text.StringBuilder
    Public whichPort As String = "COM13"
    Private com3 As IO.Ports.SerialPort = Nothing

    Public Sub OpenSerialPort()


        com3 = My.Computer.Ports.OpenSerialPort(whichPort, 256000)
        com3.ReadTimeout = 60000000


    End Sub

    Public Function ReadOneLine() As String

        Dim Incoming As String = com3.ReadLine()



        Return Incoming
    End Function


    Public Sub SendSerialData(ByVal data As String)
        ' Send strings to a serial port.

        com3.WriteLine(data)


    End Sub

    Public ComPortMutex As New System.Threading.Mutex













End Class
