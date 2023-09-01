Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class tickets
    Dim act As String = "activate"
    Dim number As String

    Dim connectionString As String = "Data Source=cit\sqlexpress;Initial Catalog=ticket;Integrated Security=True"

    Private Sub RefreshDataGridView()
        Dim dataTable As New DataTable()

        Using connection As New SqlConnection(connectionString)
            connection.Open()

            Dim query As String = "SELECT routes.id,buses.prak,directions.direction,routes.startTime,routes.date,buses.numbers,status FROM buses,directions,routes where buses.id=routes.busid and directions.id=routes.directionid"
            Using adapter As New SqlDataAdapter(query, connection)
                adapter.Fill(dataTable)
            End Using
        End Using

        ' Binding data to the DataGridView
        DataGridView1.DataSource = dataTable
    End Sub
    Private Sub routes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshDataGridView()


    End Sub





    Private Sub insertDataButton_Click(sender As Object, e As EventArgs) Handles insertDataButton.Click
        Dim success As Boolean = False
        Dim exists As Boolean = False
        Dim dir As String = Tid.Text
        Dim am As String = TextBox3.Text




        Using connection As New SqlConnection(connectionString)
            connection.Open()
            If String.IsNullOrWhiteSpace(dir) OrElse String.IsNullOrWhiteSpace(am) Then
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If



            ' If the record doesn't exist, proceed with insertion
            If Not exists Then



                Dim query As String = "INSERT INTO tickets VALUES (@dir, @am)"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@dir", dir)
                    command.Parameters.AddWithValue("@am", am)
                    'command.Parameters.AddWithValue("@id", number)


                    Dim rowsAffected As Integer = command.ExecuteNonQuery()
                    If rowsAffected > 0 Then
                        success = True
                    End If
                End Using
            End If
        End Using


        If success Then
            MessageBox.Show("Data inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            TextBox1.Clear()

            TextBox2.Clear()

            RefreshDataGridView()

            Dim dataTableb As New DataTable()

            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim queryx As String = "SELECT COUNT(id) FROM tickets"
                Using adapter As New SqlDataAdapter(queryx, connection)
                    ' No need to add a parameter to the adapter for this query
                    adapter.Fill(dataTableb)
                End Using
            End Using

            For Each row As DataRow In dataTableb.Rows
                Number = Convert.ToInt32(row(0)) ' Use index 0 to get the count value
            Next



            'MsgBox("Number of routes: " & Number)
            ' Tid.Text = number.ToString()
            Label6.Text = number
            Label8.Text = "TICKET ID IS"

        Else
            MessageBox.Show("Failed to insert data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        ' Fetching data from the database

    End Sub

    Private Sub seach_Click(sender As Object, e As EventArgs) Handles seach.Click
        Dim datex As String = TextBox2.Text
        Dim timex As String = TextBox2.Text


        If String.IsNullOrWhiteSpace(datex) OrElse String.IsNullOrWhiteSpace(timex) Then
            MessageBox.Show("Please enter a date value to search.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim dataTable As New DataTable()
        Dim act As String = "activate"
        Using connection As New SqlConnection(connectionString)
            connection.Open()

            Dim query As String = "SELECT routes.id,directions.direction,routes.startTime,routes.date,buses.numbers,directions.price FROM buses,directions,routes where buses.id=routes.busid and directions.id=routes.directionid and date = @pk and startTime=@s and status=@st"
            Using adapter As New SqlDataAdapter(query, connection)
                ' Add the parameter to the adapter
                adapter.SelectCommand.Parameters.AddWithValue("@pk", datex)
                adapter.SelectCommand.Parameters.AddWithValue("@s", timex)
                adapter.SelectCommand.Parameters.AddWithValue("@st", act)

                adapter.Fill(dataTable)
            End Using
        End Using

        ' Binding data to the DataGridView
        DataGridView1.DataSource = dataTable
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        getTicket.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        LoginForm1.Show()
        Me.Hide()
    End Sub
End Class
