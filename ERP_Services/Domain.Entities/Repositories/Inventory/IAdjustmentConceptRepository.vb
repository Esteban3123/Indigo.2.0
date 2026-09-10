'************************************************************
' Assembly         : Domain.Inventory.IGroupRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/09/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IAdjustmentConceptRepository
    Inherits IRepository(Of AdjustmentConcept)

    ''' <summary>
    ''' Obtiene un concepto de ajuste por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAdjustmentConcept(code As String) As AdjustmentConcept

    ''' <summary>
    ''' Obtiene un concepto de ajuste por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAdjustmentConceptById(id As Integer) As AdjustmentConcept

    ''' <summary>
    ''' Obtiene un concepto de ajuste por cuenta contable y centro de costo.
    ''' </summary>
    ''' <param name="conceptType"></param>
    ''' <param name="adjustmentAccountId"></param>
    ''' <param name="costCenterId"></param>
    ''' <returns></returns>
    Function GetListByAdjustmentAccountCostCenterId(conceptType As Byte, adjustmentAccountId As Integer, costCenterId As Integer) As List(Of AdjustmentConcept)

End Interface
