'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Johan Sebastian Carranza Ramos
' Created          : 22-05-2019
'
' Last Modified By : Johan Sebastian Carranza Ramos
' Last Modified On : 22-05-2019
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Importaciones"
Imports System.Security.Cryptography
Imports Presentation.CloudAgent.IndigoReference.Security
Imports Presentation.Base   
Imports Infrastructure.CrossCutting.Base
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Resources
#End Region

#Region "variable y Constructor"
Public Class PReporteAuditoriaHC

    ''' <summary>
    ''' Constructor que permite la comunicacion con la interfaz ICambiarContraseña
    ''' </summary>
    Public Sub New(ByRef iview As IReporteAuditoriaHC)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.vista = iview
        End If
    End Sub

    ''' <summary>
    ''' Variable utilizada para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz IDesbloquearUsuario
    ''' </summary>
    Dim vista As IReporteAuditoriaHC
    ''' <summary>
    ''' Variable que se utiliza para instanciar el modelo 
    ''' </summary>
    Dim modelo As MCambiarContraseña

    #End Region

#Region "Metodos"
    
    ''' <summary>
    ''' Este metodo deja los campos vacios del frontal.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer()
        With vista
            .INDPaciente = String.Empty
            .InitialDate  = Nothing
            .FinalDate = Nothing
            
        End With
    End Sub

#End Region



End Class
