'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 12-04-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities
Public Interface IContractModificationReasonRepository
    Inherits IRepository(Of ContractModificationReason)

    ''' <summary>
    ''' Obtiene una razon de otro si por codigo
    ''' </summary>
    ''' <returns>razones de otro si</returns>
    ''' <remarks></remarks>
    Function GetContractModificationReason(ByVal code As String, Optional desatach As Boolean = True) As ContractModificationReason

End Interface
