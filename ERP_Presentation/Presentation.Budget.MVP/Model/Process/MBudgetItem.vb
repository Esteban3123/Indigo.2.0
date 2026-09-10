'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 28-04-2014
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports System.ServiceModel

#End Region

''' <summary>
''' Clase que expone los metodos de servicios
''' </summary>
''' <remarks></remarks>
Public Class MBudgetItem
    Inherits ModelBaseBudget
    Implements IDisposable
    Public Shared TAG As String = "206"

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Sub New()
        MyBase.New(TAG)
    End Sub

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="tagform">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal tagform As String)
        MyBase.New(tagform)
        TAG = tagform
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtiene un rubro por código y vigencia
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <param name="BudgetaryValidityId">Id de la vigencia</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <returns></returns>
    Public Async Function GetBudgetItemByCodeAndBudgetaryValidityIdAndItemType(FinancialSourceId As Integer?, Code As String, BudgetaryValidityId As Integer, ItemType As Byte) As Task(Of ActionResult(Of Category))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetBudgetItemByFinancialSourceIdAndCodeAndBudgetaryValidityIdAndItemTypeAsync(FinancialSourceId, Code, BudgetaryValidityId, ItemType, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Listado de rubros por vigencia
    ''' </summary>
    ''' <param name="ValidityId">Id de la vigencia</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListBudgetItemsByValidityAsync(ValidityId As String, ItemType As Byte) As Task(Of List(Of Category))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.ListBudgetItemsByValidityAsync(ValidityId, ItemType, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un rubro
    ''' </summary>
    ''' <param name="Category">la entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Async Function SaveBudgetItemAsync(Category As Category) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.Category))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.SaveBudgetItemAsync(Category, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un rubro
    ''' </summary>
    ''' <param name="Category">La entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Async Function DeleteBudgetItemAsync(Category As Category) As Task(Of Domain.Base.Entities.ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.DeleteBudgetItemAsync(Category, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' cambiar estado de la entidad
    ''' </summary>
    ''' <param name="Category">The category.</param>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <returns></returns>
    Public Async Function ChangeState(Category As Domain.Entities.Category, status As Boolean) As Task(Of ActionResult(Of Category))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.ChangeStatusBudgetItemAsync(Category, status, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' funcion para obtener todos los rubros por estado
    ''' </summary>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAllBudgetItembyStatus(ByVal status As Boolean) As Task(Of List(Of Category))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetAllBudgetItemsByStateAsync(status, Me._indigoSessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' devuelve el numero de rubros existentes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function CountCategory() As Task(Of Integer)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.CountCategoriesAsync(Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' devuelve el saldo del presupuesto por id del rubro y el tipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetBalanceBudgetByCategoryIdAndRevenueTypeId(categoryId As Integer, revenueTypeId As Integer) As Task(Of Decimal)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetBalanceBudgetByCategoryIdAndRevenueTypeIdAsync(categoryId, revenueTypeId, Me._indigoSessionValues.AuditMessageWcf)
    End Function

  
    'Public Async Function GetListReportBudgetExecutionIncome(ValidityId As Integer, Month As Integer, financialSourceId As Integer, CodeCategoryStart As String, codeCategoryEnd As String) As Task(Of List(Of SP_ReportBudgetExcutionIncome_Result))
    '    Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoBudget.InnerChannel)
    '        _indigoSessionValues.AuditMessageWcf.Functional = TAG
    '        Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
    '        Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
    '        OperationContext.Current.OutgoingMessageHeaders.Add(header)
    '        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetListReportBudgetExecutionIncomeAsync(ValidityId, Month, financialSourceId, CodeCategoryStart, codeCategoryEnd)
    '    End Using
    'End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
