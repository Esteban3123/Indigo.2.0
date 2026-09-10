Imports System.IO
Imports System.Security.Cryptography.Xml
Imports System.Xml
Imports Infrastructure.CrossCutting.Signature.Utils
Imports Microsoft.Xades

Namespace Signature

    Public Class SignatureDocument

#Region "Properties"

        Private _xadesSignedXml As XadesSignedXml

        Private _document As XmlDocument

        Public Property XadesSignature() As XadesSignedXml
            Get
                Return Me._xadesSignedXml
            End Get
            Set(value As XadesSignedXml)
                Me._xadesSignedXml = value
            End Set
        End Property

        Public Property Document As XmlDocument
            Get
                Return Me._document
            End Get
            Set(value As XmlDocument)
                Me._document = value
            End Set
        End Property

#End Region

#Region "Methods"

        Public Function GetDocumentBytes() As Byte()
            CheckSignatureDocument(Me)
            Using memoryStream As MemoryStream = New MemoryStream()
                Save(memoryStream)
                Return memoryStream.ToArray()
            End Using
        End Function

        Public Sub Save(output As Stream)
            Dim xmlWriterSettings As XmlWriterSettings = New XmlWriterSettings()
            xmlWriterSettings.Encoding = New Text.UTF8Encoding()
            xmlWriterSettings.Indent = True
            Using w As XmlWriter = XmlWriter.Create(output, xmlWriterSettings)
                Document.Save(w)
            End Using
        End Sub

        Public Sub Save(fileName As String)
            CheckSignatureDocument(Me)
            Dim xmlWriterSettings As XmlWriterSettings = New XmlWriterSettings()
            xmlWriterSettings.Encoding = New Text.UTF8Encoding()
            xmlWriterSettings.Indent = True
            Using w As XmlWriter = XmlWriter.Create(fileName, xmlWriterSettings)
                Document.Save(w)
            End Using
        End Sub

        Friend Sub UpdateDocument()
            If _document Is Nothing Then
                _document = New XmlDocument()
            End If
            If _document.DocumentElement IsNot Nothing Then
                Dim xmlNode As XmlNode = _document.SelectSingleNode("//*[@Id='" + _xadesSignedXml.Signature.Id + "']")
                If xmlNode IsNot Nothing Then
                    Dim xmlNamespaceManager As XmlNamespaceManager = New XmlNamespaceManager(_document.NameTable)
                    xmlNamespaceManager.AddNamespace("xades", "http://uri.etsi.org/01903/v1.3.2#")
                    xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#")
                    Dim xmlNode2 As XmlNode = xmlNode.SelectSingleNode("ds:Object/xades:QualifyingProperties", xmlNamespaceManager)
                    Dim xmlNode3 As XmlNode = xmlNode.SelectSingleNode("ds:Object/xades:QualifyingProperties/xades:UnsignedProperties", xmlNamespaceManager)
                    If xmlNode3 IsNot Nothing
                        xmlNode3.InnerXml = _xadesSignedXml.XadesObject.QualifyingProperties.UnsignedProperties.GetXml().InnerXml
                    Else
                        xmlNode3 = _document.ImportNode(_xadesSignedXml.XadesObject.QualifyingProperties.UnsignedProperties.GetXml(), True)
                        xmlNode2.AppendChild(xmlNode3)
                    End If
                Else
                    Dim xml As XmlElement = _xadesSignedXml.GetXml()
                    Dim bytes As Byte() = XMLUtil.ApplyTransform(xml, New XmlDsigC14NTransform())
                    Dim xmlDocument As XmlDocument = New XmlDocument()
                    xmlDocument.PreserveWhitespace = True
                    xmlDocument.LoadXml(Text.Encoding.UTF8.GetString(bytes))
                    Dim newChild As XmlNode = _document.ImportNode(xmlDocument.DocumentElement, True)
                    _xadesSignedXml.GetSignatureElement().AppendChild(newChild)
                End If
            Else
                _document.LoadXml(_xadesSignedXml.GetXml().OuterXml)
            End If
        End Sub

        Friend Shared Sub CheckSignatureDocument(sigDocument As SignatureDocument)
            If sigDocument Is Nothing Then
                Throw New ArgumentNullException("sigDocument")
            End If
            If sigDocument.Document Is Nothing OrElse sigDocument.XadesSignature Is Nothing
                Throw New Exception("No existe información sobre la firma")
            End If
        End Sub

#End Region

    End Class

End Namespace