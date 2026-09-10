'************************************************************
' Assembly         : Domain.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 06-11-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IBlockRecordBillingRepository
    Inherits IRepository(Of BlockRecordBilling)

    ''' <summary>
    ''' Lista Todos los registros bloqueados
    ''' </summary>
    ''' <returns>Registros bloqueados</returns>
    Function ListAllBlockRecord() As List(Of BlockRecordBilling)

    ''' <summary>
    ''' Obtiene una registro de bloqueo según parametros
    ''' </summary>
    ''' <param name="IdForm">Id del formulario</param>
    ''' <param name="IdRecord">Id del registro</param>
    ''' <returns>Registro bloqueado</returns>
    Function GetBlockRecordByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String, Optional tracking As Boolean = True) As BlockRecordBilling

    ''' <summary>
    ''' Lista Todos los registros bloqueados por Formulario
    ''' </summary>
    ''' <param name="IdForm">Id del formulario</param>
    ''' <returns>Registros bloqueados</returns>
    Function ListBlockRecordByIdForm(IdForm As String) As List(Of BlockRecordBilling)
End Interface
