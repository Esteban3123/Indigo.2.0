'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Faiber Julian Mora D.
' Created          : 07-10-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface ICircularZeroThirtyRepository
    Inherits IRepository(Of Circular030)

    Function ListTrimesters() As Dictionary(Of Int32, String)

    Function GenerateDocument030(ByVal year As Int32, ByVal trimester As Int32, ByVal user As String) As List(Of GenerateDocument030_Result)

End Interface
