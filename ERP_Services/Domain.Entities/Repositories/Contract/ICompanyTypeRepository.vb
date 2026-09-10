'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/09/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface ICompanyTypeRepository
    Inherits IRepository(Of CompanyType)

    ''' <summary>
    ''' Obtiene todos los entidades
    ''' </summary>
    ''' <returns>Lista de los entidades</returns>
    ''' <remarks></remarks>
    Function GetAllCompanyType() As List(Of CompanyType)

    ''' <summary>
    ''' obtiene un entidad por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCompanyTypeByCode(code As String, Optional tracking As Boolean = True) As CompanyType

    ''' <summary>
    ''' obtiene un entidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCompanyTypeById(id As Integer, Optional tracking As Boolean = True) As CompanyType

End Interface
