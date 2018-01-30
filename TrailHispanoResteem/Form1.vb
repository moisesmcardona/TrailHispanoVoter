Imports System.IO
Imports System.Net
Imports System.Text
Imports DSharpPlus
Imports DSharpPlus.Entities
Imports MySql.Data.MySqlClient

Public Class Form1
    Dim MySQLString As String = String.Empty
    Private WithEvents DiscordClient As DiscordClient
    Private DiscordChannelObject As DiscordChannel
    Private WithEvents DiscordClientLogger As DebugLogger
    Private Async Sub Form1_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load
        Dim dcfg As New DiscordConfiguration
        With dcfg
            .Token = My.Computer.FileSystem.ReadAllText("token.txt")
            .TokenType = TokenType.Bot
            .LogLevel = LogLevel.Debug
            .AutoReconnect = True
        End With
        Me.DiscordClient = New DiscordClient(dcfg)
        Me.DiscordClientLogger = Me.DiscordClient.DebugLogger
        Await Me.DiscordClient.ConnectAsync()
        Dim MySQLFile As StreamReader = New StreamReader("MySQLConfig.txt")
        Dim currentline As String = ""
        Dim MySQLServer As String = ""
        Dim MySQLUser As String = ""
        Dim MySQLPassword As String = ""
        Dim MySQLDatabase As String = ""
        While MySQLFile.EndOfStream = False
            currentline = MySQLFile.ReadLine
            If currentline.Contains("server") Then
                Dim GetServer As String() = currentline.Split("=")
                MySQLServer = GetServer(1)
            ElseIf currentline.Contains("username") Then
                Dim GetUsername As String() = currentline.Split("=")
                MySQLUser = GetUsername(1)
            ElseIf currentline.Contains("password") Then
                Dim GetPassword As String() = currentline.Split("=")
                MySQLPassword = GetPassword(1)
            ElseIf currentline.Contains("database") Then
                Dim GetDatabase As String() = currentline.Split("=")
                MySQLDatabase = GetDatabase(1)
            End If
        End While
        MySQLString = "server=" & MySQLServer & ";user=" & MySQLUser & ";database=" & MySQLDatabase & ";port=3306;password=" & MySQLPassword & ";"

        LoadPosts()
    End Sub
    Private Sub SetPostNotVoted(Post As String)
        Dim SQLQuery2 As String = "UPDATE posts SET voted=2 WHERE link = '" & Post & "'"
        Dim Connection2 As MySqlConnection = New MySqlConnection(MySQLString)
        Dim Command2 As New MySqlCommand(SQLQuery2, Connection2)
        Connection2.Open()
        Command2.ExecuteNonQuery()
        Connection2.Close()
    End Sub

    Private Function GetDiscordUserID(Link As String) As String
        Dim SQLQuery As String = "SELECT userid FROM posts WHERE link='" & Link & "'"
        Dim Connection As MySqlConnection = New MySqlConnection(MySQLString)
        Dim Command As New MySqlCommand(SQLQuery, Connection)
        Connection.Open()
        Dim reader As MySqlDataReader = Command.ExecuteReader
        reader.Read()
        Dim UserID As String = reader("userid")
        Connection.Close()
        Return UserID
    End Function

    Private Sub UpdatePostStatus(Post As String)
        Dim SQLQuery2 As String = "UPDATE posts SET voted=1, traildate='" & DateTime.Now() & "' WHERE link = '" & Post & "'"
        Dim Connection2 As MySqlConnection = New MySqlConnection(MySQLString)
        Dim Command2 As New MySqlCommand(SQLQuery2, Connection2)
        Connection2.Open()
        Command2.ExecuteNonQuery()
        Connection2.Close()
    End Sub
    Private Sub Button2_Click(sender As Object, e As System.EventArgs) Handles Button2.Click
        LoadPosts()
    End Sub
    Private Sub LoadPosts()
        ListBox2.Items.Clear()
        Dim SQLQuery As String = "SELECT link FROM posts WHERE voted=0 AND suspicious=0"
        Dim Connection As MySqlConnection = New MySqlConnection(MySQLString)
        Dim Command As New MySqlCommand(SQLQuery, Connection)
        Connection.Open()
        Dim reader As MySqlDataReader = Command.ExecuteReader
        If reader.HasRows = True Then
            While reader.Read
                ListBox2.Items.Add(reader("link"))
            End While
        End If
        Connection.Close()
    End Sub

    Private Sub ListBox2_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListBox2.MouseDoubleClick
        If ListBox2.SelectedIndex >= 0 Then Process.Start("https://steemit.com/tag/@" & ListBox2.SelectedItem)
    End Sub
    Private Async Function SendMessageVotingOnPost(userid As String, link As String) As Task
        Dim Channel As DiscordChannel = Await DiscordClient.GetChannelAsync(407348378570194947)
        Dim mentionuser As DiscordUser = Await DiscordClient.GetUserAsync(userid)
        Await DiscordClient.SendMessageAsync(Channel, mentionuser.Mention & ", Tu post https://steemit.com/tag/@" & link & " está siendo votado en estos momentos. :slight_smile:")
    End Function

    Private Sub Button4_Click(sender As Object, e As System.EventArgs) Handles Button4.Click
        If ListBox3.SelectedIndex >= 0 Then
            ListBox2.Items.Add(ListBox3.SelectedItem)
            ListBox3.Items.RemoveAt(ListBox3.SelectedIndex)
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As System.EventArgs) Handles Button5.Click
        If ListBox2.SelectedIndex >= 0 Then
            SetPostNotVoted(ListBox2.SelectedItem)
            ListBox2.Items.Remove(ListBox2.SelectedItem)
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As System.EventArgs) Handles Button6.Click
        ListBox3.Items.Add(ListBox2.SelectedItem)
        ListBox2.Items.Remove(ListBox2.SelectedItem)
        If ListBox2.Items.Count > 0 Then
            ListBox2.SelectedIndex = 0
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As System.EventArgs) Handles Button7.Click
        WaitListLoop()
    End Sub
    Private Async Sub WaitListLoop()
        Dim AccountFile As StreamReader = New StreamReader("account.txt")
        Dim currentline As String = ""
        Dim Account As String = ""
        Dim Key As String = ""
        While AccountFile.EndOfStream = False
            currentline = AccountFile.ReadLine
            If currentline.Contains("account") Then
                Dim GetAccount As String() = currentline.Split("=")
                Account = GetAccount(1)
            ElseIf currentline.Contains("key") Then
                Dim GetKey As String() = currentline.Split("=")
                Key = GetKey(1)
            End If
        End While
        While ListBox3.Items.Count > 0
            Dim UserID As String = GetDiscordUserID(ListBox3.Items.Item(0))
            Dim PostLink As String = ListBox3.Items.Item(0)
            Await SendMessageVotingOnPost(UserID, PostLink)
            If VotePost(PostLink, Account, Key) Then
                Threading.Thread.Sleep(10000)
            Else
                Threading.Thread.Sleep(3000)
            End If
            ListBox3.Items.RemoveAt(0)
        End While
    End Sub
    Dim Counter As Integer = 0
    Private Function VotePost(Identifier As String, Account As String, Key As String) As Boolean
        Dim Voted As Boolean = False
        Try
            Dim request As System.Net.WebRequest = System.Net.WebRequest.Create("https://api.steem.place/vote/")
            request.Method = "POST"
            Dim postData As String = "i=" & Identifier & "&w=10&v=" & Account & "&pk=" & Key
            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = byteArray.Length
            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()
            Dim response As WebResponse = request.GetResponse()
            dataStream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            reader.Close()
            dataStream.Close()
            response.Close()
            If responseFromServer.Contains("ok") Then
                UpdatePostStatus(ListBox3.Items.Item(0))
                Voted = True
            Else
                Voted = False
            End If
        Catch ex As Exception
            Voted = False
        End Try
        Return Voted
    End Function

    Private Sub Button8_Click(sender As Object, e As System.EventArgs) Handles Button8.Click
        If ListBox2.Items.Count >= 30 Then
            For i As Integer = 0 To 29
                Process.Start("https://steemit.com/tag/@" & ListBox2.Items.Item(i))
                Threading.Thread.Sleep(300)
            Next
        Else
            For i As Integer = 0 To ListBox2.Items.Count - 1
                Process.Start("https://steemit.com/tag/@" & ListBox2.Items.Item(i))
                Threading.Thread.Sleep(300)
            Next
        End If
    End Sub

    Private Sub ListBox3_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListBox3.MouseDoubleClick
        If ListBox3.SelectedIndex >= 0 Then Process.Start("https://steemit.com/tag/@" & ListBox3.SelectedItem)
    End Sub
End Class
