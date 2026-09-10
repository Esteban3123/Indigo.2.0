'************************************************************
' Assembly         : Domain.Inventory.IGroupRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/09/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IOtherWithholdingDeductionRepository
    Inherits IRepository(Of OtherWithholdingDeduction)

    ''' <summary>
    ''' Obtiene otras deducciones y retenciones por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetOtherWithholdingDeduction(code As String) As OtherWithholdingDeduction

    ''' <summary>
    ''' Obtiene otras deducciones y retenciones por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetOtherWithholdingDeductionById(id As Integer) As OtherWithholdingDeduction

    ''' <summary>
    ''' Obtiene un listado de las deducciones y retenciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListOtherWithholdingDeduction() As List(Of OtherWithholdingDeduction)

End Interface
