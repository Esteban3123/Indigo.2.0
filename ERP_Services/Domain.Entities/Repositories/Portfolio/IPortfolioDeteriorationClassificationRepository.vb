'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Oscar Astudillo Reyes
' Created          : 2024-11-10
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base
Imports Domain.Entities
#End Region

Public Interface IPortfolioDeteriorationClassificationRepository
    Inherits IRepository(Of PortfolioDeteriorationClassification)

#Region "Methods"

    ''' <summary>
    ''' Obtiene la clasificación de deterioro de cartera por código
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    Function GetPortfolioDeteriorationClassificationByCode(ByVal code As String) As PortfolioDeteriorationClassification

#End Region

End Interface
