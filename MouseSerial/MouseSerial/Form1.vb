Public Class Form1
    Dim sp As New SerialPort
    Dim g1 As Graphics
    Dim g2 As Graphics
    Dim g3 As Graphics
    Dim g4 As Graphics
    Dim g5 As Graphics
    Dim p As New Pen(Color.Red, 3)
    Dim p2 As New Pen(Color.Blue, 3)
    Dim cli As New ClassLibrary1.Class1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        g1 = PictureBox1.CreateGraphics
        g2 = PictureBox2.CreateGraphics
        g3 = PictureBox3.CreateGraphics
        g4 = PictureBox4.CreateGraphics
        g5 = PictureBox5.CreateGraphics
        sp.OpenSerialPort()
        Do
            Dim line As String = sp.ReadOneLine()
            line = line.Replace(vbCr, "")
            If TextBox1.TextLength > 1000 Then TextBox1.Text = TextBox1.Text.Substring(100)
            TextBox1.AppendText(line & vbCrLf)
            TextBox1.Refresh()


            Dim minIndex As Integer = 0
            Dim maxIndex As Integer = 9

            Dim tokens() As String = line.Split(",")
            If tokens.Count <> maxIndex - minIndex + 1 Then Throw New Exception


            Label8.Text = tokens(NumericUpDown1.Value)
            Label8.Refresh()
            Application.DoEvents()




            ReDim cli.a(maxIndex)
            For i = minIndex To maxIndex
                cli.a(i) = CInt(tokens(i - minIndex))
            Next

            cli.sw = CInt(tokens(tokens.Count - 1))

            cli.loop()

            TextBox2.Text = "Xcen= " & cli.Xcen.ToString("F2") & vbCrLf & "Ycen= " & cli.Ycen.ToString("F2") & vbCrLf & "Zval= " & cli.Zval.ToString("F2") & vbCrLf
            TextBox2.AppendText("ZoneAAbsOffsetX=" & cli.ZoneAAbsOffsetX & vbCrLf)
            TextBox2.AppendText("ZoneAAbsOffsetY=" & cli.ZoneAAbsOffsetY & vbCrLf)
            TextBox2.AppendText("ZoneAAbsX=" & cli.ZoneAAbsX & vbCrLf)
            TextBox2.AppendText("ZoneAAbsY=" & cli.ZoneAAbsY & vbCrLf)
            TextBox2.AppendText("ZoneBAbsOffsetX=" & cli.ZoneBAbsOffsetX & vbCrLf)
            TextBox2.AppendText("ZoneBAbsOffsetY=" & cli.ZoneBAbsOffsetY & vbCrLf)
            TextBox2.AppendText("ZoneBAbsX=" & cli.ZoneBAbsX & vbCrLf)
            TextBox2.AppendText("ZoneBAbsY=" & cli.ZoneBAbsY & vbCrLf)
            TextBox2.AppendText("ZoneAMixFraction=" & cli.ZoneAMixFraction & vbCrLf)
            TextBox2.AppendText("ZoneBMixFraction=" & cli.ZoneBMixFraction & vbCrLf)
            TextBox2.AppendText("OveralConf=" & cli.OverallConf & vbCrLf)


            TextBox2.Refresh()

            DrawZoneXY(1.0, cli.Xcen, cli.Ycen, PictureBox1, g1)
            DrawZField(cli.Zval, PictureBox1, g1, Color.Red)

            DrawZoneXY(0.5, cli.XZoneA, cli.YZoneA, PictureBox2, g2, cli.InterpolatedRadius)
            DrawZField(cli.ZoneAMixFraction, PictureBox2, g2, Color.Black)

            DrawZoneXY(0.5, cli.zoneDx, cli.zoneDy, PictureBox3, g3)
            DrawZField(cli.ZoneBMixFraction, PictureBox3, g3, Color.Black)

            DrawZoneXY(0.5, cli.XZoneA, cli.YZoneA, PictureBox4, g4)
            DrawCorners(cli.XZoneB, cli.YZoneB, PictureBox4, g4)



            '            g5.Clear(Color.LightGray)
            '           DrawCross(4, cli.ZoneAAbsOffsetX, cli.ZoneAAbsOffsetY, PictureBox5, g5, Color.Red)
            '          DrawCross(4, cli.ZoneBAbsOffsetX, cli.ZoneBAbsOffsetY, PictureBox5, g5, Color.Blue)

            '            DrawDot(4, cli.ZoneAAbsX, cli.ZoneAAbsY, PictureBox5, g5, Color.Red)
            '            DrawDot(4, cli.ZoneBAbsX, cli.ZoneBAbsY, PictureBox5, g5, Color.Blue)

            '            DrawCircle(4, cli.WeightedX, cli.WeightedY, PictureBox5, g5, Color.Green)

            '            Dim tempCol As Color = Color.Green
            '            If cli.OverallConf < 0.1 Then tempCol = Color.Orange
            '            If cli.OverallConf < 0.03 Then tempCol = Color.Red

            '            DrawZField(cli.OverallConf, PictureBox5, g5, tempCol)



            g5.Clear(Color.LightGray)
            DrawCross(4, cli.ZoneAAbsOffsetX, cli.ZoneAAbsOffsetY, PictureBox5, g5, Color.Red)
            DrawCross(4, cli.ZoneBAbsOffsetX, cli.ZoneBAbsOffsetY, PictureBox5, g5, Color.Blue)


            If CheckBox1.Checked Then
                DrawDot(4, cli.ZoneAAbsX1, cli.ZoneAAbsY1, PictureBox5, g5, Color.Red)
                DrawDot(4, cli.ZoneBAbsX1, cli.ZoneBAbsY1, PictureBox5, g5, Color.Blue)
                DrawCircle(4, cli.MouseX1, cli.MouseY1, PictureBox5, g5, Color.Green)
                CheckBox1.Text = "Show1 " & cli.OverallConfidence1.ToString
            End If



            If CheckBox2.Checked Then
                DrawDot(4, cli.ZoneAAbsX2, cli.ZoneAAbsY2, PictureBox5, g5, Color.Red)
                DrawDot(4, cli.ZoneBAbsX2, cli.ZoneBAbsY2, PictureBox5, g5, Color.Blue)
                DrawCircle(4, cli.MouseX2, cli.MouseY2, PictureBox5, g5, Color.Green)
                CheckBox2.Text = "Show2 " & cli.OverallConfidence2.ToString
            End If

            If CheckBox3.Checked Then
                DrawDot(4, cli.ZoneAAbsX3, cli.ZoneAAbsY3, PictureBox5, g5, Color.Red)
                DrawDot(4, cli.ZoneBAbsX3, cli.ZoneBAbsY3, PictureBox5, g5, Color.Blue)
                DrawCircle(4, cli.MouseX3, cli.MouseY3, PictureBox5, g5, Color.Green)
            End If

            If CheckBox4.Checked Then
                DrawDot(4, cli.ZoneAAbsX4, cli.ZoneAAbsY4, PictureBox5, g5, Color.Red)
                DrawDot(4, cli.ZoneBAbsX4, cli.ZoneBAbsY4, PictureBox5, g5, Color.Blue)
                DrawCircle(4, cli.MouseX4, cli.MouseY4, PictureBox5, g5, Color.Green)
            End If

            DrawDot(4, cli.MidpointX12, cli.MidpointY12, PictureBox5, g5, Color.Purple)



            Dim tempCol As Color = Color.Green
            If cli.OverallConf < 0.1 Then tempCol = Color.Orange
            If cli.OverallConf < 0.03 Then tempCol = Color.Red

            DrawZField(cli.OverallConf, PictureBox5, g5, tempCol)


            Application.DoEvents()




        Loop
    End Sub


    Public Sub DrawZField(z As Single, pb As PictureBox, g As Graphics, whichCol As Color)
        Dim tempPen As New Pen(whichCol, 3)
        Dim fullscale As Single = pb.Height / 2
        Dim yzero As Integer = pb.Height \ 2
        g.DrawLine(tempPen, 2, yzero, 2, yzero - z * fullscale)

    End Sub


    Public Sub DrawZoneXY(scale As Single, x As Single, y As Single, pb As PictureBox, g As Graphics, Optional radius As Single = Single.MaxValue)
        Dim fullscale As Single = pb.Width / 2 / scale
        g.Clear(Color.LightGray)
        Dim xzero As Integer = pb.Width \ 2
        Dim yzero As Integer = pb.Height \ 2
        g.DrawLine(p, xzero, yzero, xzero + x * fullscale, yzero - y * fullscale)
        If radius <> Single.MaxValue Then
            Dim r As Single = fullscale * radius
            g.DrawEllipse(p2, xzero - r, yzero - r, 2 * r, 2 * r)
        End If

    End Sub


    Public Sub DrawCorners(x As Single, y As Single, pb As PictureBox, g As Graphics)
        Dim fullscale As Single = pb.Width / 2
        Dim startX, startY As Single

        If x >= 0 Then
            If y >= 0 Then
                startX = 0
                startY = pb.Height
            Else  ' y<0
                startX = 0
                startY = 0
            End If
        Else  ' x<0
            If y >= 0 Then
                startX = pb.Width
                startY = pb.Height
            Else  ' y<0
                startX = pb.Width
                startY = 0
            End If
        End If

        g.DrawLine(p, startX, startY, startX + x * fullscale, startY - y * fullscale)

    End Sub




    Public Sub DrawDot(scale As Single, x As Single, y As Single, pb As PictureBox, g As Graphics, whichCol As Color)
        Dim tempPen As New Pen(whichCol, 3)
        Dim fullscale As Single = pb.Width / 2 / scale

        Dim xzero As Integer = pb.Width \ 2
        Dim yzero As Integer = pb.Height \ 2
        g.DrawEllipse(tempPen, xzero + x * fullscale - 1, yzero - y * fullscale - 1, 3, 3)

    End Sub


    Public Sub DrawCircle(scale As Single, x As Single, y As Single, pb As PictureBox, g As Graphics, whichCol As Color)
        Dim tempPen As New Pen(whichCol, 1)
        Dim fullscale As Single = pb.Width / 2 / scale

        Dim xzero As Integer = pb.Width \ 2
        Dim yzero As Integer = pb.Height \ 2
        g.DrawEllipse(tempPen, xzero + x * fullscale - 4, yzero - y * fullscale - 4, 9, 9)

    End Sub



    Public Sub DrawCross(scale As Single, x As Single, y As Single, pb As PictureBox, g As Graphics, whichCol As Color)
        Dim narrowPen As New Pen(whichCol, 1)
        Dim fullscale As Single = pb.Width / 2 / scale

        Dim xzero As Integer = pb.Width \ 2
        Dim yzero As Integer = pb.Height \ 2
        g.DrawLine(narrowPen, xzero + x * fullscale - 10, yzero - y * fullscale, xzero + x * fullscale + 10, yzero - y * fullscale)
        g.DrawLine(narrowPen, xzero + x * fullscale, yzero - y * fullscale - 10, xzero + x * fullscale, yzero - y * fullscale + 10)

    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click

    End Sub
End Class
