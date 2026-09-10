'************************************************************
' Assembly         : Domain.Contract
' Author           : Giovanny Plazas L
' Created          : 24/08/2021
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IContractPackageRepository
    Inherits IRepository(Of ContractPackage)

    ''' <summary>
    ''' Obtiene una entidad cups por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetContractPackage(code As String) As ContractPackage

    ''' <summary>
    ''' Obtiene una entidad cups por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetContractPackageById(id As Integer, Optional ByVal tracking As Boolean = True) As ContractPackage

End Interface
