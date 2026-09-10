Imports System.Reflection
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates

Namespace Crypto

    Public Class Signer
        Implements IDisposable

#Region "Properties"

        Private _disposeCryptoProvider As Boolean

        Private _signingCertificate As X509Certificate2

        Private _signingKey As AsymmetricAlgorithm

        Private _x509Chain As X509Chain

        Public ReadOnly Property Certificate As X509Certificate2
            Get
                Return Me._signingCertificate
            End Get
        End Property

        Public ReadOnly Property SigningKey As AsymmetricAlgorithm
            Get
                Return Me._signingKey
            End Get
        End Property

        Public Property CertChain As X509Chain
            Get
                Return Me._x509Chain
            End Get
            Set(value As X509Chain)
                Me._x509Chain = value
            End Set
        End Property

#End Region

#Region "Builder"

        Public Sub New(certificate As X509Certificate2)
            If certificate Is Nothing
                Throw New ArgumentNullException("certificate")
            End If
            If Not certificate.HasPrivateKey
                Throw New Exception("Certificate has not Private Key")
            End If
            Me._signingCertificate = certificate
            Me.SetSigningKey(_signingCertificate)
        End Sub

        Public Sub New(certificate As X509Certificate2, x509Chain As X509Chain)
            If certificate Is Nothing
                Throw New ArgumentNullException("certificate")
            End If
            If Not certificate.HasPrivateKey
                Throw New Exception("Certificate has not Private Key")
            End If
            If x509Chain Is Nothing
                Throw New ArgumentNullException("x509Chain")
            End If
            If x509Chain.ChainStatus.Any()
                Dim chainStatus = x509Chain.ChainStatus.First()
                If chainStatus.Status.HasFlag(X509ChainStatusFlags.Revoked) OrElse chainStatus.Status.HasFlag(X509ChainStatusFlags.NotTimeValid)
                    Throw New Exception(chainStatus.StatusInformation)
                End If
            End If

            Me._x509Chain = x509Chain
            Me._signingCertificate = certificate
            Me.SetSigningKey(_signingCertificate)
        End Sub

#End Region

#Region "Methods"

        Private Sub SetSigningKey(certificate As X509Certificate2)
            Dim rSACryptoServiceProvider As RSACryptoServiceProvider = CType(certificate.PrivateKey, RSACryptoServiceProvider)
            If rSACryptoServiceProvider.CspKeyContainerInfo.ProviderName = CryptoConst.MS_STRONG_PROV OrElse rSACryptoServiceProvider.CspKeyContainerInfo.ProviderName = CryptoConst.MS_ENHANCED_PROV OrElse rSACryptoServiceProvider.CspKeyContainerInfo.ProviderName = CryptoConst.MS_DEF_PROV OrElse rSACryptoServiceProvider.CspKeyContainerInfo.ProviderName = CryptoConst.MS_DEF_RSA_SCHANNEL_PROV Then
                Dim typeFromHandle As Type = GetType(CspKeyContainerInfo)
                Dim field As FieldInfo = typeFromHandle.GetField("m_parameters", BindingFlags.Instance Or BindingFlags.NonPublic)
                Dim cspParameters As CspParameters = CType(field.GetValue(rSACryptoServiceProvider.CspKeyContainerInfo), CspParameters)
                Dim cspParameters2 As CspParameters = New CspParameters(24, CryptoConst.MS_ENH_RSA_AES_PROV, rSACryptoServiceProvider.CspKeyContainerInfo.KeyContainerName)
                cspParameters2.KeyNumber = cspParameters.KeyNumber
                cspParameters2.Flags = cspParameters.Flags
                _signingKey = New RSACryptoServiceProvider(cspParameters2)
                _disposeCryptoProvider = True
            Else
                _signingKey = rSACryptoServiceProvider
                _disposeCryptoProvider = false
            End If
        End Sub

#End Region

#Region "Dispose"

        Public Sub Dispose() Implements IDisposable.Dispose
            If _disposeCryptoProvider AndAlso _signingKey IsNot Nothing Then
                _signingKey.Dispose()
            End If
        End Sub

#End Region

    End Class

End NameSpace