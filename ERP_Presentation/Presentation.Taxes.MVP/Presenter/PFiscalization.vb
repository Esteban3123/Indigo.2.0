'***********************************************************************
' Assembly         : Presentacion.Taxes.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/09/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo

#End Region

Public Class PFiscalization

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IFiscalization

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IFiscalization)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        Me.View = iview
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    Public Function GetCountTempFile() As Integer
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).TaxesService.GetCountTempFile()
    End Function

    Public Function ListViewDifferenceDIANAndPrivateLiquidation(Year As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).TaxesService.ListViewDifferenceDIANAndPrivateLiquidation(Year)
    End Function

    Public Function ListViewDiscountReteIcaAndPrivateDeclaration(Year As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).TaxesService.ListViewDiscountReteIcaAndPrivateDeclaration(Year)
    End Function

    Public Function ListViewOmissives(Year As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).TaxesService.ListViewOmissives(Year)
    End Function

#End Region

End Class
