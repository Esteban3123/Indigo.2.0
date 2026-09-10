Imports System.IO
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Cryptography.Xml
Imports System.Xml
Imports Infrastructure.CrossCutting.Signature.Signature
Imports Infrastructure.CrossCutting.Signature.Signature.Parameters
Imports Infrastructure.CrossCutting.Signature.Utils
Imports Infrastructure.CrossCutting.Signature.Validation
Imports Microsoft.Xades
Imports Org.BouncyCastle.Asn1
Imports Org.BouncyCastle.Asn1.X509

Public Class XadesService

#Region "Properties"

    Private _refContent As Reference

    Private _dataFormat As DataObjectFormat

#End Region

#Region "Methods"

    Public Function Sign(input As Stream, parameters As SignatureParameters) As SignatureDocument
        If parameters.Signer Is Nothing Then
            Throw New Exception("Es necesario un certificado válido para la firma")
        End If
        If input Is Nothing AndAlso String.IsNullOrEmpty(parameters.ExternalContentUri) Then
            Throw New Exception("No se ha especificado ningún contenido a firmar")
        End If
        Dim signatureDocument As SignatureDocument = New SignatureDocument()
        Me._dataFormat = new DataObjectFormat()
        Select Case parameters.SignaturePackaging
            Case SignaturePackaging.INTERNALLY_DETACHED
                If parameters.DataFormat Is Nothing OrElse String.IsNullOrEmpty(parameters.DataFormat.MimeType)
                    Throw New NullReferenceException("Se necesita especificar el tipo MIME del elemento a firmar.")
                End If
                Me._dataFormat.MimeType = parameters.DataFormat.MimeType
                Me._dataFormat.Encoding = IIf(parameters.DataFormat.MimeType = "text/xml", "UTF-8", "http://www.w3.org/2000/09/xmldsig#base64")
                If Not String.IsNullOrEmpty(parameters.ElementIdToSign) Then
                    Me.SetContentInternallyDetached(signatureDocument, XMLUtil.LoadDocument(input), parameters.ElementIdToSign)
                Else
                    Me.SetContentInternallyDetached(signatureDocument, input)
                End If
            Case SignaturePackaging.HASH_INTERNALLY_DETACHED
                Me._dataFormat.MimeType = IIf(parameters.DataFormat Is Nothing OrElse String.IsNullOrEmpty(parameters.DataFormat.MimeType), "application/octet-stream", parameters.DataFormat.MimeType)
                Me._dataFormat.Encoding = "http://www.w3.org/2000/09/xmldsig#base64"
                Me.SetContentInternallyDetachedHashed(signatureDocument, input)
            Case SignaturePackaging.EXTERNALLY_DETACHED
                Me.SetContentExternallyDetached(signatureDocument, parameters.ExternalContentUri)
            Case SignaturePackaging.ENVELOPED
                Me._dataFormat.MimeType = "text/xml"
                Me._dataFormat.Encoding = "UTF-8"
                Me.SetContentEnveloped(signatureDocument, XMLUtil.LoadDocument(input))
            Case SignaturePackaging.ENVELOPING
                Me._dataFormat.MimeType = "text/xml"
                Me._dataFormat.Encoding = "UTF-8"
                Me.SetContentEveloping(signatureDocument, XMLUtil.LoadDocument(input))
        End Select

        If parameters.DataFormat IsNot Nothing
            If Not String.IsNullOrEmpty(parameters.DataFormat.TypeIdentifier)
                Me._dataFormat.ObjectIdentifier = New ObjectIdentifier()
                Me._dataFormat.ObjectIdentifier.Identifier.IdentifierUri = parameters.DataFormat.TypeIdentifier
            End If
			Me._dataFormat.Description = parameters.DataFormat.Description
        End If

        Me.SetSignatureId(signatureDocument.XadesSignature)
        Me.PrepareSignature(signatureDocument, parameters)
        signatureDocument.XadesSignature.ComputeSignature()
        Me.UpdateXadesSignature(signatureDocument)
        Return signatureDocument
    End Function

    Public Function CoSign(sigDocument As SignatureDocument, parameters As SignatureParameters) As SignatureDocument
        Signature.SignatureDocument.CheckSignatureDocument(sigDocument)
        Me._refContent = sigDocument.XadesSignature.GetContentReference()
        If Me._refContent Is Nothing Then
            Throw New Exception("No se ha podido encontrar la referencia del contenido firmado.")
        End If
        Me._dataFormat = Nothing
        For Each item As DataObjectFormat In sigDocument.XadesSignature.XadesObject.QualifyingProperties.SignedProperties.SignedDataObjectProperties.DataObjectFormatCollection
            If item.ObjectReferenceAttribute = "#" + Me._refContent.Id
                Me._dataFormat = New DataObjectFormat()
				Me._dataFormat.Encoding = item.Encoding
				Me._dataFormat.MimeType = item.MimeType				
				Me._dataFormat.Description = item.Description
				If item.ObjectIdentifier IsNot Nothing Then
					Me._dataFormat.ObjectIdentifier = New ObjectIdentifier()
					Me._dataFormat.ObjectIdentifier.Identifier.IdentifierUri = item.ObjectIdentifier.Identifier.IdentifierUri
					Me._dataFormat.ObjectIdentifier.Description = item.ObjectIdentifier.Description
				End If
                Exit For
            End If
        Next
        Dim signatureDocument As SignatureDocument = New SignatureDocument()
        signatureDocument.Document = CType(sigDocument.Document.Clone(), XmlDocument)
        signatureDocument.Document.PreserveWhitespace = True
        signatureDocument.XadesSignature = New XadesSignedXml(signatureDocument.Document)
        signatureDocument.XadesSignature.LoadXml(sigDocument.XadesSignature.GetXml())
        Dim parentNode As XmlNode = signatureDocument.XadesSignature.GetSignatureElement().ParentNode
        signatureDocument.XadesSignature = New XadesSignedXml(signatureDocument.Document)
        Me._refContent.Id = "Reference-" + Guid.NewGuid().ToString()
        If Me._refContent.Type <> "http://www.w3.org/2000/09/xmldsig#Object"
            Me._refContent.Type = ""
        End If
        signatureDocument.XadesSignature.AddReference(Me._refContent)
		signatureDocument.XadesSignature.SignatureNodeDestination = Iif(parentNode.NodeType = XmlNodeType.Document, CType(parentNode, XmlDocument).DocumentElement, CType(parentNode, XmlElement))
        Me.SetSignatureId(signatureDocument.XadesSignature)
        Me.PrepareSignature(signatureDocument, parameters)
        signatureDocument.XadesSignature.ComputeSignature()
        Me.UpdateXadesSignature(signatureDocument)
        Return signatureDocument
    End Function

    Public Function CounterSign(sigDocument As SignatureDocument, parameters As SignatureParameters) As SignatureDocument
        If parameters.Signer Is Nothing
            Throw New Exception("Es necesario un certificado válido para la firma.")
        End If
        Signature.SignatureDocument.CheckSignatureDocument(sigDocument)
        Dim signatureDocument As SignatureDocument = New SignatureDocument()
        signatureDocument.Document = CType(sigDocument.Document.Clone(), XmlDocument)
        signatureDocument.Document.PreserveWhitespace = True
        Dim xadesSignedXml As XadesSignedXml = New XadesSignedXml(signatureDocument.Document)
        Me.SetSignatureId(xadesSignedXml)
        xadesSignedXml.SigningKey = parameters.Signer.SigningKey
        Me._refContent = New Reference()
        Me._refContent.Uri = "#" + sigDocument.XadesSignature.SignatureValueId
        Me._refContent.Id = "Reference-" + Guid.NewGuid().ToString()
        Me._refContent.Type = "http://uri.etsi.org/01903#CountersignedSignature"
        Me._refContent.AddTransform(New XmlDsigC14NTransform())
        xadesSignedXml.AddReference(Me._refContent)
        Me._dataFormat = new DataObjectFormat()
		Me._dataFormat.MimeType = "text/xml"
		Me._dataFormat.Encoding = "UTF-8"
        Dim keyInfo As KeyInfo = New KeyInfo()
        keyInfo.Id = "KeyInfoId-" + xadesSignedXml.Signature.Id
        keyInfo.AddClause(CType(New KeyInfoX509Data(CType(parameters.Signer.Certificate, X509Certificate)), KeyInfoClause))
        keyInfo.AddClause(CType(New RSAKeyValue(CType(parameters.Signer.SigningKey, RSA)), KeyInfoClause))
        xadesSignedXml.KeyInfo = keyInfo
        xadesSignedXml.AddReference(New Reference() With
            {
                .Id = "ReferenceKeyInfo-" + xadesSignedXml.Signature.Id,
                .Uri = "#KeyInfoId-" + xadesSignedXml.Signature.Id
            }
        )
        Dim xadesObject As XadesObject = New XadesObject()
        xadesObject.Id = "CounterSignatureXadesObject-" + Guid.NewGuid().ToString()
        xadesObject.QualifyingProperties.Target = "#" + xadesSignedXml.Signature.Id
        xadesObject.QualifyingProperties.SignedProperties.Id = "SignedProperties-" + xadesSignedXml.Signature.Id
        Me.AddSignatureProperties(signatureDocument, xadesObject.QualifyingProperties.SignedProperties.SignedSignatureProperties, xadesObject.QualifyingProperties.SignedProperties.SignedDataObjectProperties, xadesObject.QualifyingProperties.UnsignedProperties.UnsignedSignatureProperties, parameters)
        xadesSignedXml.AddXadesObject(xadesObject)
        For Each reference2 As Reference In xadesSignedXml.SignedInfo.References
            reference2.DigestMethod = parameters.DigestMethod.URI
        Next
        xadesSignedXml.SignedInfo.SignatureMethod = parameters.SignatureMethod.URI
        xadesSignedXml.AddXadesNamespace = True
        xadesSignedXml.ComputeSignature()
        signatureDocument.XadesSignature = New XadesSignedXml(signatureDocument.Document)
        signatureDocument.XadesSignature.LoadXml(sigDocument.XadesSignature.GetXml())
        Dim unsignedProperties As UnsignedProperties = signatureDocument.XadesSignature.UnsignedProperties
        unsignedProperties.UnsignedSignatureProperties.CounterSignatureCollection.Add(xadesSignedXml)
        signatureDocument.XadesSignature.UnsignedProperties = unsignedProperties
        Me.UpdateXadesSignature(signatureDocument)
        Return signatureDocument
    End Function

    Public Function Load(input As Stream) As SignatureDocument()
        Return Load(XMLUtil.LoadDocument(input))
    End Function

    Public Function Load(fileName As String) As SignatureDocument()
        Using input As FileStream = New FileStream(fileName, FileMode.Open)
            Return Load(input)
        End Using
    End Function

    Public Function Load(xmlDocument As XmlDocument) As SignatureDocument()
        Dim elementsByTagName As XmlNodeList = xmlDocument.GetElementsByTagName("Signature", "http://www.w3.org/2000/09/xmldsig#")
        If elementsByTagName.Count = 0
            Throw New Exception("No se ha encontrado ninguna firma.")
        End If
        Dim list As List(Of SignatureDocument) = New List(Of SignatureDocument)
        For Each item In elementsByTagName
            Dim signatureDocument As SignatureDocument = New SignatureDocument()
            signatureDocument.Document = CType(xmlDocument.Clone(), XmlDocument)
            signatureDocument.Document.PreserveWhitespace = True
            signatureDocument.XadesSignature = New XadesSignedXml(signatureDocument.Document)
            signatureDocument.XadesSignature.LoadXml(CType(item, XmlElement))
            list.Add(signatureDocument)
        Next
        Return list.ToArray()
    End Function

    Public Function Validate(sigDocument As SignatureDocument) As ValidationResult
        SignatureDocument.CheckSignatureDocument(sigDocument)
        Dim xadesValidator As XadesValidator = New XadesValidator()
        Return xadesValidator.Validate(sigDocument)
    End Function

    Private Sub SetSignatureId(xadesSignedXml As XadesSignedXml)
        Dim str As String = Guid.NewGuid().ToString()
        xadesSignedXml.Signature.Id = "xmldsig-" + str
        xadesSignedXml.SignatureValueId = "sigvalue-xmldsig-" + str
    End Sub

    Private Sub SetContentInternallyDetached(sigDocument As SignatureDocument, xmlDocument As XmlDocument, elementId As String)
        sigDocument.Document = xmlDocument
        Me._refContent = New Reference()
        Me._refContent.Uri = "#" + elementId
        Me._refContent.Id = "Reference-" + Guid.NewGuid().ToString()
        If Me._dataFormat.MimeType = "text/xml" Then
            Dim Transform As XmlDsigC14NTransform = New XmlDsigC14NTransform()
            Me._refContent.AddTransform(Transform)
        Else
            Dim XmlDsigBase64Transform As XmlDsigBase64Transform = New XmlDsigBase64Transform()
            Me._refContent.AddTransform(XmlDsigBase64Transform)
        End If
        sigDocument.XadesSignature = New XadesSignedXml(sigDocument.Document)
        sigDocument.XadesSignature.AddReference(Me._refContent)
    End Sub

    Private Sub SetContentInternallyDetached(sigDocument As SignatureDocument, input As Stream)
        sigDocument.Document = New XmlDocument()
        Dim xmlElement As XmlElement = sigDocument.Document.CreateElement("DOCFIRMA")
        sigDocument.Document.AppendChild(CType(xmlElement, XmlNode))
        Dim str As String = "CONTENT-" + Guid.NewGuid().ToString()
        Me._refContent = New Reference()
        Me._refContent.Uri = "#" + str
        Me._refContent.Id = "Reference-" + Guid.NewGuid().ToString()
        Me._refContent.Type = "http://www.w3.org/2000/09/xmldsig#Object"
        Dim xmlElement2 As XmlElement = sigDocument.Document.CreateElement("CONTENT")
        If Me._dataFormat.MimeType = "text/xml" Then
            Dim xmlDocument As XmlDocument = New XmlDocument()
            xmlDocument.PreserveWhitespace = True
            xmlDocument.Load(input)
            xmlElement2.InnerXml = xmlDocument.DocumentElement.OuterXml
            Dim transform As XmlDsigC14NTransform = New XmlDsigC14NTransform()
            Me._refContent.AddTransform(transform)
        Else
            Dim xmlDsigBase64Transform As XmlDsigBase64Transform = New XmlDsigBase64Transform()
            Me._refContent.AddTransform(xmlDsigBase64Transform)
            Using memoryStream As MemoryStream = New MemoryStream()
				input.CopyTo(memoryStream)
				xmlElement2.InnerText = Convert.ToBase64String(memoryStream.ToArray(), Base64FormattingOptions.InsertLineBreaks)
			End Using
        End If
        xmlElement2.SetAttribute("Id", str)
        xmlElement2.SetAttribute("MimeType", Me._dataFormat.MimeType)
        xmlElement2.SetAttribute("Encoding", Me._dataFormat.Encoding)
        xmlElement.AppendChild(CType(xmlElement2, XmlNode))
        sigDocument.XadesSignature = New XadesSignedXml(sigDocument.Document)
        sigDocument.XadesSignature.AddReference(Me._refContent)
    End Sub

	Private Sub SetContentInternallyDetachedHashed(sigDocument As SignatureDocument, input As Stream)
      sigDocument.Document = new XmlDocument()
      Dim xmlElement As XmlElement = sigDocument.Document.CreateElement("DOCFIRMA")
      sigDocument.Document.AppendChild(xmlElement)
      Dim str As String = "CONTENT-" + Guid.NewGuid().ToString()
      Me._refContent = new Reference()
      Me._refContent.Uri = "#" + str
      Me._refContent.Id = "Reference-" + Guid.NewGuid().ToString()
      Me._refContent.Type = "http://www.w3.org/2000/09/xmldsig#Object"
      Dim xmlElement2 As XmlElement = sigDocument.Document.CreateElement("CONTENT")
	  Dim xmlDsigBase64Transform As XmlDsigBase64Transform = New XmlDsigBase64Transform()
      Me._refContent.AddTransform(xmlDsigBase64Transform)
      Using shA256 As SHA256 = SHA256.Create()
        xmlElement2.InnerText = Convert.ToBase64String(shA256.ComputeHash(input))
	  End Using
      xmlElement2.SetAttribute("Id", str)
      xmlElement2.SetAttribute("MimeType", Me._dataFormat.MimeType)
      xmlElement2.SetAttribute("Encoding", Me._dataFormat.Encoding)
      xmlElement.AppendChild(CType(xmlElement2, XmlNode))
      sigDocument.XadesSignature = new XadesSignedXml(sigDocument.Document)
      sigDocument.XadesSignature.AddReference(Me._refContent)
    End Sub
	
    Private Sub SetContentEveloping(sigDocument As SignatureDocument, xmlDocument As XmlDocument)
        Me._refContent = new Reference()
        sigDocument.XadesSignature = new XadesSignedXml()
        Dim xmlDocument2 As XmlDocument = CType(xmlDocument.Clone(), XmlDocument)
        xmlDocument2.PreserveWhitespace = true
        If xmlDocument2.ChildNodes(0).NodeType = XmlNodeType.XmlDeclaration
            xmlDocument2.RemoveChild(xmlDocument2.ChildNodes(0))
        End If
        Dim str As String = "DataObject-" + Guid.NewGuid().ToString()        
        sigDocument.XadesSignature.AddObject(new DataObject() With
			{
				.Data = xmlDocument2.ChildNodes,
				.Id = str
			}
		)
        Me._refContent.Id = "Reference-" + Guid.NewGuid().ToString()
        Me._refContent.Uri = "#" + str
        Me._refContent.Type = "http://www.w3.org/2000/09/xmldsig#Object"
        Dim transform As XmlDsigC14NTransform = new XmlDsigC14NTransform()
        Me._refContent.AddTransform(transform)
        sigDocument.XadesSignature.AddReference(Me._refContent)
    End Sub

    Private Sub SetSignatureDestination(sigDocument As SignatureDocument, destination As SignatureXPathExpression)
        Dim xmlNode As XmlNode
        If destination.Namespaces.Count > 0 Then
            Dim xmlNamespaceManager As XmlNamespaceManager = new XmlNamespaceManager(sigDocument.Document.NameTable)
            For Each _namespace As KeyValuePair(Of String, String) In destination.Namespaces
                xmlNamespaceManager.AddNamespace(_namespace.Key, _namespace.Value)
            Next
            xmlNode = sigDocument.Document.SelectSingleNode(destination.XPathExpression, xmlNamespaceManager)
        Else
            xmlNode = sigDocument.Document.SelectSingleNode(destination.XPathExpression)
        End If
        If xmlNode Is Nothing Then
            throw new Exception("Elemento no encontrado")
        End If
        sigDocument.XadesSignature.SignatureNodeDestination = CType(xmlNode, XmlElement)
    End Sub

    Private Sub SetContentExternallyDetached(sigDocument As SignatureDocument, fileName As String)
        Me._refContent = new Reference()
        sigDocument.Document = new XmlDocument()
        sigDocument.XadesSignature = new XadesSignedXml(sigDocument.Document)
        Me._refContent.Uri = new Uri(fileName).AbsoluteUri
        Me._refContent.Id = "Reference-" + Guid.NewGuid().ToString()
        If Me._refContent.Uri.EndsWith(".xml") OrElse Me._refContent.Uri.EndsWith(".XML")
            Me._dataFormat.MimeType = "text/xml"
            Me._refContent.AddTransform(new XmlDsigC14NTransform())
        End If
        sigDocument.XadesSignature.AddReference(Me._refContent)
    End Sub

    Private Sub AddXPathTransform(sigDocument As SignatureDocument, namespaces As Dictionary(Of String, String), XPathString As String)
        Dim xmlDocument As XmlDocument = Iif(sigDocument.Document Is Nothing, new XmlDocument(), sigDocument.Document)
        Dim xmlElement As XmlElement = xmlDocument.CreateElement("XPath")
        For Each _namespace As KeyValuePair(Of String, String) in namespaces
            Dim xmlAttribute As XmlAttribute = xmlDocument.CreateAttribute("xmlns:" + _namespace.Key)
            xmlAttribute.Value = _namespace.Value
            xmlElement.Attributes.Append(xmlAttribute)
        Next
        xmlElement.InnerText = XPathString
        Dim xmlDsigXPathTransform As XmlDsigXPathTransform = new XmlDsigXPathTransform()
        xmlDsigXPathTransform.LoadInnerXml(xmlElement.SelectNodes("."))
        Dim reference As Reference = CType(sigDocument.XadesSignature.SignedInfo.References(0), Reference)
        reference.AddTransform(xmlDsigXPathTransform)
    End Sub

    Private Sub SetContentEnveloped(sigDocument As SignatureDocument, xmlDocument As XmlDocument)
        sigDocument.Document = xmlDocument
        Me._refContent = new Reference()
        sigDocument.XadesSignature = new XadesSignedXml(sigDocument.Document)
        Me._refContent.Id = "Reference-" + Guid.NewGuid().ToString()
        Me._refContent.Uri = ""
        Me._dataFormat = Nothing
        For i As Integer = 0 To sigDocument.Document.DocumentElement.Attributes.Count - 1
            If sigDocument.Document.DocumentElement.Attributes(i).Name.Equals("id", StringComparison.InvariantCultureIgnoreCase)
                Me._refContent.Uri = "#" + sigDocument.Document.DocumentElement.Attributes(i).Value
                Exit For
            End If
        Next
        Dim transform As XmlDsigEnvelopedSignatureTransform = new XmlDsigEnvelopedSignatureTransform()
        Me._refContent.AddTransform(transform)
        sigDocument.XadesSignature.AddReference(Me._refContent)
    End Sub

    Private Sub PrepareSignature(sigDocument As SignatureDocument, parameters As SignatureParameters)
        sigDocument.XadesSignature.SignedInfo.SignatureMethod = parameters.SignatureMethod.URI
        Me.AddCertificateInfo(sigDocument, parameters)
        Me.AddXadesInfo(sigDocument, parameters)
        For Each reference As Reference In sigDocument.XadesSignature.SignedInfo.References
            reference.DigestMethod = parameters.DigestMethod.URI
        Next
        If parameters.SignatureDestination IsNot Nothing
            Me.SetSignatureDestination(sigDocument, parameters.SignatureDestination)
        End If
        If parameters.XPathTransformations.Count > 0
            For Each xPathTransformation As SignatureXPathExpression in parameters.XPathTransformations
                Me.AddXPathTransform(sigDocument, xPathTransformation.Namespaces, xPathTransformation.XPathExpression)
            Next
        End If
    End Sub
	
	Private Sub UpdateXadesSignature(sigDocument As SignatureDocument)
      sigDocument.UpdateDocument()
      Dim xmlElement As XmlElement = CType(sigDocument.Document.SelectSingleNode("//*[@Id='" + sigDocument.XadesSignature.Signature.Id + "']"), XmlElement)
      sigDocument.XadesSignature = new XadesSignedXml(sigDocument.Document)
      sigDocument.XadesSignature.LoadXml(xmlElement)
    End Sub

    Private Sub AddXadesInfo(sigDocument As SignatureDocument, parameters As SignatureParameters)
        Dim xadesObject As XadesObject = new XadesObject()
        xadesObject.Id = "XadesObjectId-" + Guid.NewGuid().ToString()
        xadesObject.QualifyingProperties.Target = "#" + sigDocument.XadesSignature.Signature.Id
        xadesObject.QualifyingProperties.SignedProperties.Id = "SignedProperties-" + sigDocument.XadesSignature.Signature.Id
        Me.AddSignatureProperties(sigDocument, xadesObject.QualifyingProperties.SignedProperties.SignedSignatureProperties, xadesObject.QualifyingProperties.SignedProperties.SignedDataObjectProperties, xadesObject.QualifyingProperties.UnsignedProperties.UnsignedSignatureProperties, parameters)
        sigDocument.XadesSignature.AddXadesObject(xadesObject)
    End Sub

    Private Sub AddCertificateInfo(sigDocument As SignatureDocument, parameters As SignatureParameters)
        sigDocument.XadesSignature.SigningKey = parameters.Signer.SigningKey
        Dim keyInfo As KeyInfo = new KeyInfo()
        keyInfo.Id = "KeyInfo" '"KeyInfoId-" + sigDocument.XadesSignature.Signature.Id
        keyInfo.AddClause(new KeyInfoX509Data(parameters.Signer.Certificate))
        sigDocument.XadesSignature.KeyInfo = keyInfo
        sigDocument.XadesSignature.AddReference(New Reference() With {.Id = "ReferenceKeyInfo", .Uri = String.Format("#{0}", keyInfo.Id)})
    End Sub

    Private Sub AddSignatureProperties(sigDocument As SignatureDocument, signedSignatureProperties As SignedSignatureProperties, signedDataObjectProperties As SignedDataObjectProperties, unsignedSignatureProperties As UnsignedSignatureProperties, parameters As SignatureParameters)
        If parameters.Signer.CertChain IsNot Nothing Then            
            For Each chainElement As X509ChainElement In parameters.Signer.CertChain.ChainElements
                Dim objectToAdd As Cert = new Cert()
                Dim strResult = Me.CreateValidIssuerName(chainElement.Certificate.IssuerName.Name)
                objectToAdd.IssuerSerial.X509IssuerName = strResult.ObjectEmbbeded
                objectToAdd.IssuerSerial.X509SerialNumber = chainElement.Certificate.GetSerialNumberAsDecimalString()
                DigestUtil.SetCertDigest(chainElement.Certificate.GetRawCertData(), parameters.DigestMethod, objectToAdd.CertDigest)
                signedSignatureProperties.SigningCertificate.CertCollection.Add(objectToAdd)

                If Not strResult.StateResult Then
                    Exit For
                End If
            Next
        Else
            Dim objectToAdd As Cert = new Cert()
            Dim strResult = Me.CreateValidIssuerName(parameters.Signer.Certificate.IssuerName.Name)
            objectToAdd.IssuerSerial.X509IssuerName = strResult.ObjectEmbbeded
            objectToAdd.IssuerSerial.X509SerialNumber = parameters.Signer.Certificate.GetSerialNumberAsDecimalString()
            DigestUtil.SetCertDigest(parameters.Signer.Certificate.GetRawCertData(), parameters.DigestMethod, objectToAdd.CertDigest)
            signedSignatureProperties.SigningCertificate.CertCollection.Add(objectToAdd)
        End If

        If parameters.SignaturePolicyInfo IsNot Nothing Then
            If Not String.IsNullOrEmpty(parameters.SignaturePolicyInfo.PolicyIdentifier) Then
                signedSignatureProperties.SignaturePolicyIdentifier.SignaturePolicyImplied = false
                signedSignatureProperties.SignaturePolicyIdentifier.SignaturePolicyId.SigPolicyId.Identifier.IdentifierUri = parameters.SignaturePolicyInfo.PolicyIdentifier
            End If
            If Not String.IsNullOrEmpty(parameters.SignaturePolicyInfo.PolicyUri)
                Dim objectToAdd As SigPolicyQualifier = new SigPolicyQualifier()
                objectToAdd.AnyXmlElement = sigDocument.Document.CreateElement(XadesSignedXml.XmlXadesPrefix, "SPURI", "http://uri.etsi.org/01903/v1.3.2#")
                objectToAdd.AnyXmlElement.InnerText = parameters.SignaturePolicyInfo.PolicyUri
                signedSignatureProperties.SignaturePolicyIdentifier.SignaturePolicyId.SigPolicyQualifiers.SigPolicyQualifierCollection.Add(objectToAdd)
            End If
            If Not String.IsNullOrEmpty(parameters.SignaturePolicyInfo.PolicyHash)
                signedSignatureProperties.SignaturePolicyIdentifier.SignaturePolicyId.SigPolicyHash.DigestMethod.Algorithm = parameters.SignaturePolicyInfo.PolicyDigestAlgorithm.URI
                signedSignatureProperties.SignaturePolicyIdentifier.SignaturePolicyId.SigPolicyHash.DigestValue = Convert.FromBase64String(parameters.SignaturePolicyInfo.PolicyHash)
            End If
        End If

        Dim signatureProperties As SignedSignatureProperties = signedSignatureProperties
        Dim signingDate As DateTime? = parameters.SigningDate
        Dim now As DateTime
        If Not signingDate.HasValue
            now = DateTime.Now
        Else
            signingDate = parameters.SigningDate
            now = signingDate.Value
        End If

        signatureProperties.SigningTime = now
        If Me._dataFormat IsNot Nothing
            Dim objectToAdd As DataObjectFormat = new DataObjectFormat()
            objectToAdd.MimeType = Me._dataFormat.MimeType
            objectToAdd.Encoding = Me._dataFormat.Encoding
            objectToAdd.Description = Me._dataFormat.Description
            objectToAdd.ObjectReferenceAttribute = "#" + Me._refContent.Id
            If Me._dataFormat.ObjectIdentifier IsNot Nothing Then
                objectToAdd.ObjectIdentifier.Identifier.IdentifierUri = Me._dataFormat.ObjectIdentifier.Identifier.IdentifierUri
            End If
            signedDataObjectProperties.DataObjectFormatCollection.Add(objectToAdd)
        End If

        If parameters.SignerRole IsNot Nothing AndAlso (parameters.SignerRole.CertifiedRoles.Count > 0 OrElse parameters.SignerRole.ClaimedRoles.Count > 0) Then
            signedSignatureProperties.SignerRole = new Microsoft.Xades.SignerRole()
            For Each certifiedRole As X509Certificate In parameters.SignerRole.CertifiedRoles
                Dim certifiedRoleCollection As CertifiedRoleCollection = signedSignatureProperties.SignerRole.CertifiedRoles.CertifiedRoleCollection
                Dim objectToAdd As CertifiedRole = new CertifiedRole()
                objectToAdd.PkiData = certifiedRole.GetRawCertData()
                certifiedRoleCollection.Add(objectToAdd)
            Next
            For Each claimedRole As String In parameters.SignerRole.ClaimedRoles
                signedSignatureProperties.SignerRole.ClaimedRoles.ClaimedRoleCollection.Add(new ClaimedRole() With
                    {
                        .InnerText = claimedRole
                    }
                )
            Next
        End If
        For Each signatureCommitment As SignatureCommitment In parameters.SignatureCommitments
            Dim objectToAdd As CommitmentTypeIndication = new CommitmentTypeIndication()
            objectToAdd.CommitmentTypeId.Identifier.IdentifierUri = signatureCommitment.CommitmentType.URI
            objectToAdd.AllSignedDataObjects = True
            For Each commitmentTypeQualifier As XmlElement In signatureCommitment.CommitmentTypeQualifiers
                objectToAdd.CommitmentTypeQualifiers.CommitmentTypeQualifierCollection.Add(New CommitmentTypeQualifier() With
                    {
                        .AnyXmlElement = commitmentTypeQualifier
                    }
                )
            Next
            signedDataObjectProperties.CommitmentTypeIndicationCollection.Add(objectToAdd)
        Next

        If parameters.SignatureProductionPlace IsNot Nothing
            signedSignatureProperties.SignatureProductionPlace.City = parameters.SignatureProductionPlace.City
            signedSignatureProperties.SignatureProductionPlace.StateOrProvince = parameters.SignatureProductionPlace.StateOrProvince
            signedSignatureProperties.SignatureProductionPlace.PostalCode = parameters.SignatureProductionPlace.PostalCode
            signedSignatureProperties.SignatureProductionPlace.CountryName = parameters.SignatureProductionPlace.CountryName
        End If
    End Sub

    Private Function CreateValidIssuerName(subject As String) As ObjectResult(Of String)
        Try
            Dim x509Name As X509Name = New X509Name(subject)

            Dim str = String.Empty
            Me.GetPortionIssuerName(x509Name, X509Name.C, NameOf(X509Name.C), str)
            Me.GetPortionIssuerName(x509Name, X509Name.L, NameOf(X509Name.L), str)
            Me.GetPortionIssuerName(x509Name, X509Name.O, NameOf(X509Name.O), str)
            Me.GetPortionIssuerName(x509Name, X509Name.OU, NameOf(X509Name.OU), str)
            Me.GetPortionIssuerName(x509Name, X509Name.CN, NameOf(X509Name.CN), str)
            Me.GetPortionIssuerName(x509Name, X509Name.E, X509Name.E.ToString(), str)

            Return New ObjectResult(Of String) With { .StateResult = True, .ObjectEmbbeded = str}
        Catch ex As Exception
            'subject = subject.Replace(",S=",",ST=").Replace(", S=", ", ST=").Replace(", S =", ", ST =")
            Return New ObjectResult(Of String) With { .StateResult = False, .ObjectEmbbeded = subject}
        End Try
    End Function

    Private Sub GetPortionIssuerName(x509Name As X509Name, derIdentifier As DerObjectIdentifier, letter As String, ByRef str As String)
        If x509Name.GetValueList(derIdentifier).Count > 0 Then
            If Not String.IsNullOrEmpty(str) Then
                str += ","
            End If
            str += letter + "=" + x509Name.GetValueList(derIdentifier)(0)
        End If
    End Sub

#End Region

End Class
