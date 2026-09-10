Imports System.IO
Imports System.Security.Cryptography
Imports System.Security.Cryptography.Xml
Imports System.Xml
Imports Microsoft.Xades

Namespace Utils

    Public Class XMLUtil

#Region "Builder"

        Public Shared Function ApplyTransform(element As XmlElement, transform As System.Security.Cryptography.Xml.Transform) As Byte()
            Dim bytes As Byte() = Text.Encoding.UTF8.GetBytes(element.OuterXml)
            Using obj As MemoryStream = New MemoryStream(bytes)
                transform.LoadInput(obj)
                Using memoryStream As MemoryStream = CType(transform.GetOutput(GetType(Stream)), MemoryStream)
                    Return memoryStream.ToArray()
                End Using
            End Using
        End Function

        Public Shared Function ComputeValueOfElementList(xadesSignedXml As XadesSignedXml, elementXpaths As ArrayList) As Byte()
            Dim signatureElement As XmlElement = xadesSignedXml.GetSignatureElement()
            Dim allNamespaces As List(Of XmlAttribute) = xadesSignedXml.GetAllNamespaces(signatureElement)
            Dim ownerDocument As XmlDocument = signatureElement.OwnerDocument
            Dim xmlNamespaceManager As XmlNamespaceManager = new XmlNamespaceManager(ownerDocument.NameTable)
            xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#")
            xmlNamespaceManager.AddNamespace("xades", "http://uri.etsi.org/01903/v1.3.2#")
            Using memoryStream As MemoryStream = New MemoryStream()
                For Each elementXpath As String in elementXpaths
                    Dim xmlNodeList As XmlNodeList = signatureElement.SelectNodes(elementXpath, xmlNamespaceManager)
                    if xmlNodeList.Count = 0 Then
                        throw new CryptographicException("Element " + elementXpath + " not found while calculating hash")
                    End If
                    For Each item As XmlNode in xmlNodeList
                        Dim xmlElement As XmlElement = CType(item.Clone(), XmlElement)
                        xmlElement.SetAttribute("xmlns:" + XadesSignedXml.XmlDSigPrefix, "http://www.w3.org/2000/09/xmldsig#")
                        For Each item2 As XmlAttribute In allNamespaces
                            xmlElement.SetAttribute(item2.Name, item2.Value)
                        Next
                        Dim array As Byte() = ApplyTransform(xmlElement, new XmlDsigC14NTransform())
                        memoryStream.Write(array, 0, array.Length)
                    Next
                Next
                return memoryStream.ToArray()
            End Using
        End Function

        Public Shared Function LoadDocument(input As Stream) As XmlDocument
            Dim xmlDocument As XmlDocument = new XmlDocument()
            xmlDocument.PreserveWhitespace = true
            xmlDocument.Load(input)
            return xmlDocument
        End Function

#End Region

    End Class

End Namespace