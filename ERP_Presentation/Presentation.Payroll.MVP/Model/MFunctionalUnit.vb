'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 08-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Payroll.Entities
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Domain.Base.Entities

#End Region
''' <summary>
''' Modelo de conexion con los servicios distribuidos de las unidades funcionales
''' </summary>
Public Class MFunctionalUnit
    Inherits ModelBase
    Implements IDisposable

    Public Shared TAG As String = "523"

    Sub New(tag As String)
        MyBase.New(tag)
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Funcion para obtener la unidad Funcional
    ''' </summary>
    ''' <param name="code">Codigo de la unidad funcional</param>
    ''' <returns></returns>
    Public Async Function GetFuncUnitAsync(ByVal code As String) As Task(Of FunctionalUnit)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFunctionalUnitAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Funcion para obtener la unidad Funcional por el id
    ''' </summary>
    ''' <param name="id">id de la unidad funcional</param>
    ''' <returns></returns>
    Public Async Function GetFunctionalUnitByIdAsync(ByVal id As String) As Task(Of FunctionalUnit)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFunctionalUnitByIdAsync(id, Indigo)
    End Function

    ''' <summary>
    ''' Funcion para obtener la unidad Funcional
    ''' </summary>
    ''' <param name="code">Codigo de la unidad Funcional</param>
    ''' <returns></returns>
    Public Function GetFuncUnit(ByVal code As String) As FunctionalUnit
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFunctionalUnit(code, Indigo)
    End Function

    ''' <summary>
    ''' Funcion para guardar la unidad Funcional
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <returns></returns>
    Public Function SaveFuncUnit(ByVal Record As FunctionalUnit, idSequence As Long) As ActionResult(Of FunctionalUnit)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveFunctionalUnit(Record, Indigo, idSequence)
    End Function

    ''' <summary>
    ''' Funcion para guardar la unidad Funcional Asincrono
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <returns></returns>
    Public Async Function SaveFuncUnitAsync(ByVal Record As FunctionalUnit, idSequence As Long) As Task(Of ActionResult(Of FunctionalUnit))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveFunctionalUnitAsync(Record, Indigo, idSequence)
    End Function

    ''' <summary>
    ''' Funcion para borrar la unidad Funcional
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <returns></returns>
    Public Function DeleteFuncUnit(ByVal Record As FunctionalUnit) As ActionMessageResult(Of FunctionalUnit)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteFunctionalUnit(Record, Indigo)
    End Function

    ''' <summary>
    ''' Funcion para borrar la unidad Funcional asincrono
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <returns></returns>
    Public Async Function DeleteFuncUnitAsync(ByVal Record As FunctionalUnit) As Task(Of ActionMessageResult(Of FunctionalUnit))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteFunctionalUnitAsync(Record, Indigo)
    End Function

    ''' <summary>
    ''' Metodo para retornar los campos null y customizar el formulario
    ''' </summary>
    Public Function GetFieldsNULL() As Object
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("FunctionalUnit", Indigo)
    End Function
    ''' <summary>
    ''' Metodo para Listar las unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAll() As List(Of FunctionalUnit)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllFunctionalUnit(Indigo)
    End Function
    ''' <summary>
    ''' Metodo asincrono para Listar las unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListAllAsync() As Task(Of List(Of FunctionalUnit))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllFunctionalUnitAsync(Indigo)
    End Function

    ''' <summary>
    ''' Lista todos los centros de costos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllCostCenter() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCostCenterByState(True)
    End Function

    ''' <summary>
    ''' Lista todos los centros de Producción
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllProductionCenter() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InteropCostService.ListProductionCenter()
    End Function

    ''' <summary>
    ''' Lista todos la Estructura Contable de Nómina
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllAccountingStructure() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetAccountingStructure()
    End Function

    ''' <summary>
    ''' Lista todos los usuarios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllUser(ByVal container As String) As DevExpress.Data.Linq.LinqInstantFeedbackSource
        'Return XpoServiceEx.Instance(container).SecurityService.GetAllUser()
        Return XpoServiceEx.Instance(container).SecurityService.ListUserByContainer(Indigo.IndigoContainerId)
    End Function

    Public Async Function UpdateStateFunctionalUnitAsync(functionalUnitCode As String, status As Boolean) As Task(Of ActionResult(Of FunctionalUnit))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.UpdateStateFunctionalUnitAsync(functionalUnitCode, status, Indigo)
    End Function

    Public Function ListProductionCenter() As XPInstantFeedbackSource
        If String.IsNullOrEmpty(Indigo.InteropCostContainer) Then
            Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CostService.ListCostProductionCenterByStatus(True)
        Else
            Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InteropCostService.ListProductionCenterByStatus(True)
        End If
    End Function

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
