'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.Taxes
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Xpo.Base
Imports System.IO
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Linq
Imports System.Configuration
Imports Infrastructure.Data.Xpo.AccountingRepository

#End Region

''' <summary>
''' Clase que expone los servicios de los repositorios
''' </summary>
Public Class TaxesServiceXpoEx
    Inherits XpoBaseService
    Implements IDisposable

#Region "Public Methods"

    Public Function GetCountResultLoadPlaneCollection() As Integer
        Dim session As New IndigoXPOSession(Of ResultLoadPlaneCollectionXpo)()
        Dim count As Integer = session.Evaluate(GetType(ResultLoadPlaneCollectionXpo), CriteriaOperator.Parse("Count()"), Nothing)
        Return count
    End Function

    Public Function GetCountTempFile() As Integer
        Dim session As New IndigoXPOSession(Of TempFileXpo)()
        Dim count As Integer = session.Evaluate(GetType(TempFileXpo), CriteriaOperator.Parse("Count()"), Nothing)
        Return count
    End Function

    ''' <summary>
    ''' Lista todos los ingresos de activos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListTaxesLiquidationDetailByIds(Ids As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TaxesLiquidationDetailXpo)()
        Dim textArray() = Split(Ids, ",")
            Dim criteria As CriteriaOperator = New BetweenOperator("Id", textArray(0).ToString, textArray(1).ToString)
            'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id in (" + Ids + ")")
            Dim classEntity = session.GetClassInfo(GetType(TaxesLiquidationDetailXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Listado de diferencia entre DIAN y la liquidacion privada
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewDifferenceDIANAndPrivateLiquidation(Year As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewDifferenceDIANAndPrivateLiquidationXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Year = " & Year)
            Dim classEntity = session.GetClassInfo(GetType(ViewDifferenceDIANAndPrivateLiquidationXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Listado de descuentos reteica y declaracion privada
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewDiscountReteIcaAndPrivateDeclaration(Year As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewDiscountReteIcaAndPrivateDeclarationXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Year = " & Year)
            Dim classEntity = session.GetClassInfo(GetType(ViewDiscountReteIcaAndPrivateDeclarationXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Listado de omisos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewOmissives(Year As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewOmissivesXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Year = " & Year)
            Dim classEntity = session.GetClassInfo(GetType(ViewOmissivesXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todas las cuentas contables por filtro
    ''' </summary>
    ''' <returns></returns>
    Public Function ListMainAccountsReportByFilter(ByVal filtro As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MainAccountsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
            Dim classEntity = session.GetClassInfo(GetType(MainAccountsXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Number;Name;HandlesThirdParty;HandlesCostCenter;Status;RetencionType", criteria)
            serverMode.DefaultSorting = "Number"
        Return serverMode
    End Function

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollection(Of T)(Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As List(Of T)
        Dim result = Me.LoadCollection(Of T)(XpoDefault.DataLayer, Fun, criteria)
        result.Sort()
        Return result
    End Function

    ''' <summary>
    ''' lista gernerar reporte de facturas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewTaxes(Filter As String) As XPCollection(Of TaxesViewGenerateInvoiceReportXpo)
        Dim session = New IndigoXPOSession(Of TaxesViewGenerateInvoiceReportXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(Filter)
        Dim collect As XPCollection(Of TaxesViewGenerateInvoiceReportXpo) = New XPCollection(Of TaxesViewGenerateInvoiceReportXpo)(session, criteria)
        'collect.Sorting.Add(New SortProperty("ServiceDate", SortingDirection.Ascending))
        Return collect
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListTaxesViewMassivePersuasivePayment(Filter As String) As XPCollection(Of TaxesViewMassivePersuasivePaymentXpo)
        Dim session = New IndigoXPOSession(Of TaxesViewMassivePersuasivePaymentXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(Filter)
        Dim collect As XPCollection(Of TaxesViewMassivePersuasivePaymentXpo) = New XPCollection(Of TaxesViewMassivePersuasivePaymentXpo)(session, criteria)

        Return collect
    End Function

    ''' <summary>
    ''' Lista todos los predios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListTaxesProperty() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TaxesPropertyXpo)()
        Dim classEntity = session.GetClassInfo(GetType(TaxesPropertyXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    Public Function ListTaxesLowTaxLiquidation() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TaxesLowTaxLiquidation)()
        Dim classEntity = session.GetClassInfo(GetType(TaxesLowTaxLiquidation))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista genera los predios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListTaxesPropertyReport() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TaxesTaxesPropertyReportXpo)()
        Dim classEntity = session.GetClassInfo(GetType(TaxesTaxesPropertyReportXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;ThirdPartyId;ThirdPartyId.Nit;ThirdPartyId.Name;Addres;LandArea;BuiltArea;Status;EntityName;EconomicDestiny", Nothing)
            serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
