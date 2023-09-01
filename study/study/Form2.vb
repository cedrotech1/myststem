Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form2
    Private WithEvents printDocument As New PrintDocument()

    Dim connectionString As String = "Data Source=cit\sqlexpress;Initial Catalog=ticket;Integrated Security=True"



    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click



        Dim tidX As String = Tid.Text



        If String.IsNullOrWhiteSpace(tidX) Then
            MessageBox.Show("Please enter a Ticket value to search.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim dataTable As New DataTable()

        Using connection As New SqlConnection(connectionString)
            connection.Open()

            Dim query As String = "SELECT directions.direction,routes.startTime,routes.date,directions.price,buses.prak,tickets.name FROM buses,directions,routes,tickets where buses.id=routes.busid and directions.id=routes.directionid and tickets.rid=routes.id and tickets.id=@id"
            Using adapter As New SqlDataAdapter(query, connection)
                ' Add the parameter to the adapter
                adapter.SelectCommand.Parameters.AddWithValue("@id", tidX)


                adapter.Fill(dataTable)
                For Each row As DataRow In dataTable.Rows
                    Dim direction As String = row("direction").ToString()
                    Dim startTime As String = row("startTime").ToString()
                    Dim [date] As String = row("date")
                    Dim price As Decimal = Convert.ToDecimal(row("price"))
                    Dim prak As String = Row("prak").ToString()
                    Dim namex As String = row("name").ToString()
                    Label7.Text = namex
                    Label11.Text = direction
                    Label14.Text = price & " RWF"
                    Label12.Text = startTime
                    Label13.Text = prak
                    'Label8.Text = "TICKET ID IS"
                Next
            End Using
        End Using


    End Sub
    ' Event handler for printing
    Private Sub printDocument_PrintPage(sender As Object, e As PrintPageEventArgs) Handles printDocument.PrintPage
        ' Create a Graphics object for drawing.
        Dim g As Graphics = e.Graphics
        Dim font As New Font("Arial", 12)

        ' Define the starting position for drawing.
        Dim x As Single = 100
        Dim y As Single = 100

        ' Loop through all controls on the form and print them.
        For Each control As Control In Me.Controls
            ' Check if the control is visible and printable.
            If control.Visible AndAlso control.CanSelect Then
                ' Draw the control on the page.
                Dim bmp As New Bitmap(control.Width, control.Height)
                control.DrawToBitmap(bmp, New Rectangle(0, 0, control.Width, control.Height))
                g.DrawImage(bmp, x, y)

                ' Move to the next position.
                y += control.Height + 20 ' Adjust the spacing as needed.
            End If
        Next

        ' Check if there are more pages to print.
        e.HasMorePages = False
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim printDialog As New PrintDialog()

        If printDialog.ShowDialog() = DialogResult.OK Then
            ' Set the PrintDocument object to the selected printer
            printDocument.PrinterSettings = printDialog.PrinterSettings
            ' Start the printing process
            printDocument.Print()
        End If
    End Sub

    Private Sub DataGridView5_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView5.CellContentClick

    End Sub
End Class