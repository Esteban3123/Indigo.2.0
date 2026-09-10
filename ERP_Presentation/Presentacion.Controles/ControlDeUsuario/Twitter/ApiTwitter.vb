'***********************************************************************'
' Assembly         : Presentation.Controls                              '
' Author           : Jorge Leonardo Vernaza                             '
' Created          : 01-10-2012                                         '
'                                                                       '
' Last Modified By :                                                    '
' Last Modified On :                                                    '
' Description      :                                                    '
'                                                                       '
' Copyright        : (c) . All rights reserved.                         '
'***********************************************************************'

#Region "Librerias Importadas"
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Security.Cryptography
Imports System.Net
Imports System.IO
Imports System.Runtime.Serialization.Json
Imports System.Net.Http
Imports System.Threading.Tasks
Imports Presentation.Controls.Metadata

#End Region


Public Class ApiTwitter

    Public Shared Async Function twitterApi(query As String) As Task(Of List(Of Tweet))
        Dim oauth_consumer_key = "ttIXa6baQ5eFAcnv8RGRsw"
            Dim oauth_consumer_secret = "gOVUER34vqNkRg9yNXrMDHbW0AoqGFz7me4gKSdsPGs"
            Dim oauth_token = "1700209771-9QUpvHRSTud3MLMrqHrtFvkaAxTPfX6cQHTHIOv"
            Dim oauth_token_secret = "LYGV4yFQPFWhlRY1lcdWVhF2N19AbUIApQjAqOLNIfk"

            'Request details
            Dim oauth_version = "1.0"
            Dim oauth_signature_method = "HMAC-SHA1"
            Dim oauth_nonce = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(DateTime.Now.Ticks.ToString()))
            Dim timeSpan = DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0, _
                0, DateTimeKind.Utc)
            Dim oauth_timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString()
            Dim resource_url = "https://api.twitter.com/1.1/search/tweets.json"


            'encrypted oAuth signature
            Dim baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" & "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&{6}"

            Dim baseString = String.Format(baseFormat, oauth_consumer_key, oauth_nonce, oauth_signature_method, oauth_timestamp, oauth_token, _
                oauth_version, "q=" & Uri.EscapeDataString(query))

            baseString = String.Concat("GET&", Uri.EscapeDataString(resource_url), "&", Uri.EscapeDataString(baseString))

            'Encrypt data

            Dim compositeKey = String.Concat(Uri.EscapeDataString(oauth_consumer_secret), "&", Uri.EscapeDataString(oauth_token_secret))

            Dim oauth_signature As String
            Using hasher As New HMACSHA1(System.Text.Encoding.ASCII.GetBytes(compositeKey))
                oauth_signature = Convert.ToBase64String(hasher.ComputeHash(System.Text.Encoding.ASCII.GetBytes(baseString)))
            End Using

            'Finish Authentification header

            Dim headerFormat = "OAuth oauth_nonce=""{0}"", oauth_signature_method=""{1}"", " & "oauth_timestamp=""{2}"", oauth_consumer_key=""{3}"", " & "oauth_token=""{4}"", oauth_signature=""{5}"", " & "oauth_version=""{6}"""

            Dim authHeader = String.Format(headerFormat, Uri.EscapeDataString(oauth_nonce), Uri.EscapeDataString(oauth_signature_method), Uri.EscapeDataString(oauth_timestamp), Uri.EscapeDataString(oauth_consumer_key), Uri.EscapeDataString(oauth_token), _
                Uri.EscapeDataString(oauth_signature), Uri.EscapeDataString(oauth_version))

            resource_url = resource_url & "?q=" & query
            Dim client As New HttpClient()
            client.DefaultRequestHeaders.Add("Authorization", authHeader)




        Dim uri__1 As New Uri(resource_url, UriKind.Absolute)

        Dim response As HttpResponseMessage = Await client.GetAsync(uri__1)
        response.EnsureSuccessStatusCode()

        Using read As Stream = Await response.Content.ReadAsStreamAsync()
            Dim objectJson As New DataContractJsonSerializer(GetType(RootObject))
            read.Position = 0
            Dim twitter As RootObject = DirectCast(objectJson.ReadObject(read), RootObject)


            Dim data = (From a In twitter.statuses Select New Tweet With {.UserName = a.user.name, .Message = a.text, .ImageSource = a.user.profile_image_url
                }).ToList()

            Return data
        End Using
    End Function
End Class
