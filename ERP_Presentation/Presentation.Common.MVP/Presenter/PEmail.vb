'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Jose Luis Rojas
' Created          : 11-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Infrastructure.Data.Xpo
#End Region

''' <summary>
''' Presentador del frontal de correos electrónicos
''' </summary>
''' <remarks></remarks>
Public Class PEmail

#Region "Fields"

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IEmail

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IEmail)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Lista las direcciones de correo por persona
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeEmailXPO(IdPerson As Integer)
        View.EmailXPO = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.ListEmailByIdPerson(IdPerson)
    End Sub

#End Region

End Class
