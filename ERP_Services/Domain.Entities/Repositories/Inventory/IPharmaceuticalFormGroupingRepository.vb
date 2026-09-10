'************************************************************
' Assembly         : Domain.Inventory.IGroupRepository
' Author           : Cesar Augusto Collazos
' Created          : 29/02/2024
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Base
#End Region

Public Interface IPharmaceuticalFormGroupingRepository
    Inherits IRepository(Of PharmaceuticalFormGrouping)

    ''' <summary>
    ''' Obtiene una forma farmaceutica por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPharmaceuticalFormGrouping(code As String) As PharmaceuticalFormGrouping

    ''' <summary>
    ''' Obtiene una forma farmaceutica por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPharmaceuticalFormGroupingById(id As Integer) As PharmaceuticalFormGrouping

End Interface
