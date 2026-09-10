'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 14-03-2014
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
Imports Presentation.Controls.MVP
Imports System.ComponentModel
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo

#End Region

''' <summary>
''' Presentador del frontal Conceptos de recibo de caja
''' </summary>
Public Class PCashReceiptsConcepts

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As ICashReceiptsConcepts

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As ICashReceiptsConcepts)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Lista todas las cuentas
    ''' </summary>
    Public Sub InitializeAccountAccounting(retention As Boolean)
        Using ModelXpo As New MBusqueda
            Dim filter() As Object
            If retention = True Then
                filter = {True, True}
            Else
                filter = {False, True}
            End If
            Me.View.AccountAccountingDatasource = ModelXpo.ConsultarEntidades(eDataSource.ListAccountsByRetention, filter)
        End Using
    End Sub
    ''' <summary>
    ''' Lista las cuentas tipo resultado
    ''' </summary>
    Public Sub InitializeAccountResult()
        Using ModelXpo As New MBusqueda
            Me.View.AccountAccountingDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListAccountsByClassResult()
        End Using
    End Sub

    Public Sub InitializeRubro()
        Using ModelXpo As New MBusqueda
            'Me.View.AccountAccountingDatasource = ModelXpo.ConsultarEntidades(eDataSource.ListAccounLevel, CStr(5))
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene la cabecera de la secuencia
    ''' </summary>
    Public Async Sub GetSequence()
        Using ModelCommonTreasury As New MCommonTreasury(Me.View.MyTag)
            Me.View.Sequence = Await ModelCommonTreasury.GetSequense()
        End Using
    End Sub

End Class
