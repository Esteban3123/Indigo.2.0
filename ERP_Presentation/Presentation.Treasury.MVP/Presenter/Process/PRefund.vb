'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 18-03-2014
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
Imports Presentation.Accounting.MVP
Imports Presentation.Controls.MVP
Imports Presentation.Common.MVP

#End Region

''' <summary>
''' Presentador del frontal Notas de Tesoreria
''' </summary>
Public Class PRefund

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IRefund

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IRefund)
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
    ''' Obtiene la secuencia
    ''' </summary>
    Public Async Sub GetSequence()
        Using modelCommmonTreasury As New MCommonTreasury(View.MyTag)
            Me.View.Sequense = Await modelCommmonTreasury.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Lista todas las cajas
    ''' </summary>
    Public Sub InitializeCash()
        Using Model As New MBusqueda
            Dim filter() As Object = {Indigo.UserIndigoId, 1, True} ' tipo 0 me trae las cajas de ambos tipos
            Me.View.CashDatasource = Model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCashRegisterByUser, filter)
        End Using
    End Sub

    ' ''' <summary>
    ' ''' Carga la fecha del servidor
    ' ''' </summary>
    Public Async Sub LoadDateServer()
        Dim DateServer As Date
        Using Model As New MVoucherTransaction(View.MyTag)
            DateServer = Await Model.GetServerDate()
            Me.View.DateServer = DateServer
        End Using
    End Sub

End Class
