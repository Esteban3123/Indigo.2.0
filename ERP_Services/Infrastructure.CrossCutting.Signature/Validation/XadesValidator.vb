Imports Infrastructure.CrossCutting.Signature.Signature
Imports Infrastructure.CrossCutting.Signature.Utils
Imports Microsoft.Xades
Imports Org.BouncyCastle.Cms
Imports Org.BouncyCastle.Tsp
Imports Org.BouncyCastle.Utilities

Namespace Validation

    Public Class XadesValidator

        Public Function Validate(sigDocument As SignatureDocument) As ValidationResult
            Dim validationResult As ValidationResult = new ValidationResult()
            Try
                sigDocument.XadesSignature.CheckXmldsigSignature()
            Catch ex As Exception
                validationResult.IsValid = false
                validationResult.Message = "La verificación de la firma no ha sido satisfactoria"
                return validationResult
            End Try

            If sigDocument.XadesSignature.UnsignedProperties.UnsignedSignatureProperties.SignatureTimeStampCollection.Count > 0 Then
                    Dim timeStamp As TimeStamp = sigDocument.XadesSignature.UnsignedProperties.UnsignedSignatureProperties.SignatureTimeStampCollection(0)
                    Dim timeStampToken As TimeStampToken = new TimeStampToken(new CmsSignedData(timeStamp.EncapsulatedTimeStamp.PkiData))
                    Dim messageImprintDigest As Byte() = timeStampToken.TimeStampInfo.GetMessageImprintDigest()
                    Dim byOid As Crypto.DigestMethod = Crypto.DigestMethod.GetByOid(timeStampToken.TimeStampInfo.HashAlgorithm.ObjectID.Id)
                    Dim arrayList As ArrayList = new ArrayList()
                    arrayList.Add("ds:SignatureValue")
                    Dim b As Byte() = DigestUtil.ComputeHashValue(XMLUtil.ComputeValueOfElementList(sigDocument.XadesSignature, arrayList), byOid)
                    if Not (Arrays.AreEqual(messageImprintDigest, b))
                        validationResult.IsValid = false
                        validationResult.Message = "La huella del sello de tiempo no se corresponde con la calculada"
                        return validationResult
                    End If
            End If
            validationResult.IsValid = true
            validationResult.Message = "Verificación de la firma satisfactoria"
            return validationResult
        End Function

    End Class

End NameSpace