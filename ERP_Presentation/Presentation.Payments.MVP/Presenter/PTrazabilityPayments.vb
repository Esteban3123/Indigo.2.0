'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Rafael Eduardo Patiño
' Created          : 21/03/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP
Imports Presentation.Payroll.MVP

#End Region

Public Class PTrazabilitypayments

#Region "Variables and Constructors"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As ITrazabilityPayments

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As ITrazabilityPayments)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las ciudades
    ''' </summary>
    Public Sub InitializeSupplier()
        Dim modeltmp As New MTrazabilitypayments(Me.View.MyTag)
        Me.View.SupplierXpo = modeltmp.ListSupplierByStatus()
    End Sub

    Public Sub InitializeAccountPayable(idSupplier As Integer)
        Dim modeltmp As New MTrazabilitypayments(Me.View.MyTag)
        Me.View.AccountPayable = modeltmp.ListAccountPayablebysupplier(idSupplier)
    End Sub

    ''' <summary>
    ''' Metodo que inicializa los reembolsos
    ''' </summary>
    Public Sub InitializeRefunds()
        Me.View.RefundsXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.ListRefundByTrazability()
    End Sub

    ''' <summary>
    ''' Obtiene el reembolso por id
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetRefundById(Id As Integer) As RefundXpo
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.GetCollection(Of RefundXpo)(Nothing, filtroConsulta).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtene la trazabilidad de reembolsos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRefundsTrazability(RefundCode As String) As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetRefundsTrazability(RefundCode)
    End Function

#End Region

End Class
