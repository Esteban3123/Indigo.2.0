'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base
Imports Domain.Entities
#End Region

Public Interface IProvisionRangesRepository
    Inherits IRepository(Of ProvisionRanges)

#Region "Methods"
    ''' <summary>
    ''' Metodo para obtener rago de provision por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetProvisionRangesByCode(ByVal code As String, Optional tracking As Boolean = True)
#End Region

End Interface
