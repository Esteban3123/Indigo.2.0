'***********************************************************************
' Assembly         : Application.Budget
' Author           : Oscar Ivan Sierra Jaramillo
' Created          : 21-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ILowTaxLiquidationAdminService
    Inherits IDisposable


    ''' <summary>
    '''
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLowTaxLiquidation(consecutive As String, audit As AuditMessage) As LowTaxLiquidation

    ''' <summary>
    '''
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveLowTaxLiquidation(Entity As LowTaxLiquidation, state As Integer, audit As AuditMessage) As ActionResult(Of LowTaxLiquidation)

End Interface
