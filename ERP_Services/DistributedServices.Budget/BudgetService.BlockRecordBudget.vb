'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 04-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Budget
Imports Domain.Entities
Imports Microsoft.Practices.Unity

#End Region

Partial Class BudgetService
    Implements IBudgetServiceBlockRecordBudget

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <param name="blockRecord">Registro bloqueado</param>
    ''' <returns>ActionResult</returns>
    Public Function DeleteBlockRecord(blockRecord As BlockRecordBudget) As Domain.Base.Entities.ActionResult Implements IBudgetServiceBlockRecordBudget.DeleteBlockRecord
        Using service As IBlockRecordBudgetAdminService = Container.Current.Resolve(Of IBlockRecordBudgetAdminService)()
            Return service.DeleteBlockRecord(blockRecord)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una registro de bloqueo según parametros
    ''' </summary>
    ''' <param name="IdForm">Id del formulario</param>
    ''' <param name="IdRecord">Id del registro</param>
    ''' <returns>Registro bloqueado</returns>
    Public Function GetBlockRecordByIdformAndIdRecord(IdForm As String, IdRecord As String) As BlockRecordBudget Implements IBudgetServiceBlockRecordBudget.GetBlockRecordByIdformAndIdRecord
        Using service As IBlockRecordBudgetAdminService = Container.Current.Resolve(Of IBlockRecordBudgetAdminService)()
            Return service.GetBlockRecordByIdformAndIdRecord(IdForm, IdRecord)
        End Using
    End Function

    ''' <summary>
    ''' Lista Todos los registros bloqueados
    ''' </summary>
    ''' <returns>Registros bloqueados</returns>
    Public Function ListAllBlockRecord() As List(Of BlockRecordBudget) Implements IBudgetServiceBlockRecordBudget.ListAllBlockRecord
        Using service As IBlockRecordBudgetAdminService = Container.Current.Resolve(Of IBlockRecordBudgetAdminService)()
            Return service.ListAllBlockRecord()
        End Using
    End Function

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <param name="blockRecord">Registro bloqueado</param>
    ''' <returns>ActionResult</returns>
    Public Function SaveBlockRecord(blockRecord As BlockRecordBudget) As Domain.Base.Entities.ActionResult(Of BlockRecordBudget) Implements IBudgetServiceBlockRecordBudget.SaveBlockRecord
        Using service As IBlockRecordBudgetAdminService = Container.Current.Resolve(Of IBlockRecordBudgetAdminService)()
            Return service.SaveBlockRecord(blockRecord)
        End Using
    End Function

End Class