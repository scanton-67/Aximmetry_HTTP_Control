Imports System.Net
Imports System.Text
Imports System.IO
Imports System.Xml






'Module Module1

'    Sub Main()
'        ' Dim path As String = "c:\temp\MyTest.txt"
'        Dim path As Array
'        ' Create or overwrite the file.
'        Dim fs As FileStream = File.Create(path)
'        'Dim fs As FileStream = File.Create

'        ' Add text to the file.
'        Dim info As Byte() = New UTF8Encoding(True).GetBytes("This is some text in the file.")
'        fs.Write(info, 0, info.Length)
'        fs.Close()
'    End Sub

'End Module



Public Class Form1

    Dim logincookie As CookieContainer

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim postData As String = "referer=http%3A%2F%2Fforums.zybez.net%2Findex.php%3Fapp%3Dcore%26module%3Dglobal%26section%3Dlogin&username=" & TextBox1.Text & "&password=" & TextBox2.Text & "&rememberMe=1"
        Dim tempCookies As New CookieContainer
        Dim encoding As New UTF8Encoding
        Dim byteData As Byte() = encoding.GetBytes(postData)

        Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://forums.zybez.net/index.php?app=core&module=global§ion=login&do=process"), HttpWebRequest)
        postReq.Method = "POST"
        postReq.KeepAlive = True
        postReq.CookieContainer = tempCookies
        postReq.ContentType = "application/x-www-form-urlencoded"
        postReq.Referer = "http://forums.zybez.net/index.php?app=core&module=global§ion=login&do=process"
        postReq.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.3) Gecko/20100401 Firefox/4.0 (.NET CLR 3.5.30729)"
        postReq.ContentLength = byteData.Length

        Dim postreqstream As Stream = postReq.GetRequestStream()
        postreqstream.Write(byteData, 0, byteData.Length)
        postreqstream.Close()
        Dim postresponse As HttpWebResponse

        postresponse = DirectCast(postReq.GetResponse(), HttpWebResponse)
        tempCookies.Add(postresponse.Cookies)
        logincookie = tempCookies
        Dim postreqreader As New StreamReader(postresponse.GetResponseStream())

        Dim thepage As String = postreqreader.ReadToEnd

        RichTextBox1.Text = thepage

    End Sub


    Public Sub createStream()
        Using stream As New MemoryStream
            Using writer As New StreamWriter(stream)
                writer.Write("someText")
            End Using

            'stream can now be used anywhere a Stream is required.
        End Using
    End Sub

    'Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    '    WebBrowser1.DocumentText = RichTextBox1.Text
    'End Sub


    Sub HTTPSendReceiveDemo(ByVal msg, ByVal pin, ByVal cmnd)
        Dim xdoc As New System.Xml.XmlDocument
        Dim resp As String
        'Dim stream As MemoryStream
        'Dim writer As StreamWriter
        ''Using stream As New MemoryStream
        ''Using writer As New StreamWriter(stream)
        'writer.Write("someText")
        ''End Using
        ''End Using


        'xdoc.Load("c:\temp\temp\test2.xml")
        'xdoc.Load(Stream)

        ' Dim bdata() As Byte = System.Text.Encoding.ASCII.GetBytes(xdoc.OuterXml)

        Dim bresp() As Byte

        Dim MsgHeader As String = ("<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>")
        ' Dim MsgData As String = ("<action type=""ComposerSetPinValueAction"" Module=""Root"" Pin=""CrawlText"" Value=""" & TextBox3.Text & """ />") 'Stuart Test CTV CP24 BNNB MUCH " />")                 
        Dim MsgData As String = ("<action type=""" & cmnd & """ Module=""Root"" Pin=""" & pin & """ Value=""" & msg & """ />") 'Stuart Test CTV CP24 BNNB MUCH " />")                 


        ' Dim MsgData As String = "<?xml version=""1.0"" encoding="utf-8" standalone="yes"?>" & vbcrlf & "<action type="ComposerSetPinValueAction" Module="Root" Pin="CrawlText" Value=" Stuart Test CTV CP24 BNNB MUCH TESTESTESTYES " />"

        Dim bdata As Byte() = Encoding.ASCII.GetBytes(MsgData)

        Dim wc As New System.Net.WebClient
        wc.Headers.Add("Content-Type", "text/xml")
        ' bresp = wc.UploadData("http://ip_of_the_Aximmetry_machine:Port", bdata)
        bresp = wc.UploadData("http://127.0.0.1:21463", bdata)

        resp = System.Text.Encoding.ASCII.GetString(bresp)
        ' MsgBox(resp) '//response from the URL
        RichTextBox1.Text = resp
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim cmnd As String = "ComposerSetPinValueAction"
        Dim msg As String = TextBox3.Text
        Dim pin As String = "CrawlText"
        HTTPSendReceiveDemo(msg, pin, cmnd)
    End Sub


    'Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
    '    Dim cmnd As String = "ComposerSetPinValueAction"
    '    Dim msg As String = TextBox4.Text

    '    Dim pin As String = "HeadlineText"
    '    HTTPSendReceiveDemo(msg, pin, cmnd)
    'End Sub

    ' Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim writer As New XmlTextWriter("C:\temp\temp\test.xml", System.Text.Encoding.UTF8)
        writer.WriteStartDocument(True)
        writer.Formatting = Formatting.Indented
        writer.Indentation = 2
        writer.WriteStartElement("action")
        createNode(1, "action", "tyope", writer)
        ' createNode(2, "Product 2", "2000", writer)
        ' createNode(3, "Product 3", "3000", writer)
        ' createNode(4, "Product 4", "4000", writer)
        writer.WriteEndElement()
        writer.WriteEndDocument()

        writer.Close()
    End Sub


    Private Sub createNode(ByVal pID As String, ByVal pName As String, ByVal pPrice As String, ByVal writer As XmlTextWriter)
        writer.WriteStartElement("action")
        writer.WriteStartElement("Product_id")
        writer.WriteString(pID)
        'writer.WriteEndElement()
        'writer.WriteStartElement("Product_name")
        'writer.WriteString(pName)
        'writer.WriteEndElement()
        'writer.WriteStartElement("Product_price")
        'writer.WriteString(pPrice)
        'writer.WriteEndElement()
        'writer.WriteEndElement()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim cmnd As String = "ComposerSetPinValueAction"
        Dim msg As String = TextBox4.Text

        Dim pin As String = "HeadlineText"
        HTTPSendReceiveDemo(msg, pin, cmnd)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim cmnd As String = "ComposerSetPinValueAction"
        Dim msg As String = TextBox5.Text
        Dim pin As String = "TitleText"
        HTTPSendReceiveDemo(msg, pin, cmnd)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim cmnd As String = "ComposerGetPinValueAction"
        Dim msg As String = "TitleText"
        Dim pin As String = TextBox6.Text
        HTTPSendReceiveDemo(msg, pin, cmnd)
    End Sub

    Dim rssContent As StringBuilder = New StringBuilder()






    Private Function ParseRssFile() As String
        Dim rssXmlDoc As XmlDocument = New XmlDocument()
        rssXmlDoc.Load("http://feeds.feedburner.com/techulator/articles")
        Dim rssNodes As XmlNodeList = rssXmlDoc.SelectNodes("rss/channel/item")
        'Dim rssContent As StringBuilder = New StringBuilder()

        For Each rssNode As XmlNode In rssNodes
            Dim rssSubNode As XmlNode = rssNode.SelectSingleNode("title")
            Dim title As String = If(rssSubNode IsNot Nothing, rssSubNode.InnerText, "")
            rssSubNode = rssNode.SelectSingleNode("link")
            Dim link As String = If(rssSubNode IsNot Nothing, rssSubNode.InnerText, "")
            rssSubNode = rssNode.SelectSingleNode("description")
            Dim description As String = If(rssSubNode IsNot Nothing, rssSubNode.InnerText, "")
            rssContent.Append("<a href='" & link & "'>" & title & "</a><br>" & description)
        Next

        Return rssContent.ToString()
    End Function

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click


        ParseRssFile()
        RichTextBox1.Text = rssContent.ToString()
    End Sub






    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim cmnd As String = "ComposerSetPinValueAction"
        Dim msg As String = "True"
        Dim pin As String = "On"
        HTTPSend(msg, pin, cmnd)
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim cmnd As String = "ComposerSetPinValueAction"
        Dim msg As String = "True"
        Dim pin As String = "Logo"
        HTTPSend(msg, pin, cmnd)
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim cmnd As String = "ComposerSetPinValueAction"
        Dim msg As String = "True"
        Dim pin As String = "Headline"
        HTTPSend(msg, pin, cmnd)
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim cmnd As String = "ComposerSetPinValueAction"
        Dim msg As String = "True"
        Dim pin As String = "Weather"
        HTTPSend(msg, pin, cmnd)
    End Sub


    Sub HTTPSend(ByVal msg, ByVal pin, ByVal cmnd)
        Dim xdoc As New System.Xml.XmlDocument
        Dim resp As String
        'Dim stream As MemoryStream
        'Dim writer As StreamWriter
        ''Using stream As New MemoryStream
        ''Using writer As New StreamWriter(stream)
        'writer.Write("someText")
        ''End Using
        ''End Using


        'xdoc.Load("c:\temp\temp\test2.xml")
        'xdoc.Load(Stream)

        ' Dim bdata() As Byte = System.Text.Encoding.ASCII.GetBytes(xdoc.OuterXml)

        Dim bresp() As Byte

        Dim MsgHeader As String = ("<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>")
        ' Dim MsgData As String = ("<action type=""ComposerSetPinValueAction"" Module=""Root"" Pin=""CrawlText"" Value=""" & TextBox3.Text & """ />") 'Stuart Test CTV CP24 BNNB MUCH " />")                 
        'Dim MsgData As String = ("<action type=""" & cmnd & """ Module=""Root"" Button=""" & pin & """ />") 'Stuart Test CTV CP24 BNNB MUCH " />")   

        ' Dim MsgData As String = ("<action type =""" & cmnd & """ Module=""Root"" Button=""" & pin & """ State=""True"" />")

        Dim MsgData As String = ("<action type=""" & cmnd & """ Module=""Root"" Pin=""" & pin & """ Value=""" & msg & """ />") 'Stuart Test CTV CP24 BNNB MUCH " />")                 


        ' Dim MsgData As String = "<?xml version=""1.0"" encoding="utf-8" standalone="yes"?>" & vbcrlf & "<action type="ComposerSetPinValueAction" Module="Root" Pin="CrawlText" Value=" Stuart Test CTV CP24 BNNB MUCH TESTESTESTYES " />"

        Dim bdata As Byte() = Encoding.ASCII.GetBytes(MsgData)

        Dim wc As New System.Net.WebClient
        wc.Headers.Add("Content-Type", "text/xml")
        ' bresp = wc.UploadData("http://ip_of_the_Aximmetry_machine:Port", bdata)
        bresp = wc.UploadData("http://127.0.0.1:21463", bdata)

        resp = System.Text.Encoding.ASCII.GetString(bresp)
        ' MsgBox(resp) '//response from the URL
        RichTextBox1.Text = resp
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim cmnd As String = "ComposerSetPinValueAction"
        Dim msg As String = "False"
        Dim pin As String = "On"
        HTTPSend(msg, pin, cmnd)
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim cmnd As String = "ComposerSetPinValueAction"
        Dim msg As String = "False"
        Dim pin As String = "Logo"
        HTTPSend(msg, pin, cmnd)
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim cmnd As String = "ComposerSetPinValueAction"
        Dim msg As String = "False"
        Dim pin As String = "Headline"
        HTTPSend(msg, pin, cmnd)
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Dim cmnd As String = "ComposerSetPinValueAction"
        Dim msg As String = "False"
        Dim pin As String = "Weather"
        HTTPSend(msg, pin, cmnd)
    End Sub


End Class