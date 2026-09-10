'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 25-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface ICompanyRepository
    Inherits IRepository(Of Company)

    ''' <summary>
    ''' Obtiene todos las Compañías
    ''' </summary>
    ''' <returns>Lista de Compañías</returns>
    Function ListAllCompany() As List(Of Company)

    ''' <summary>
    ''' Obtiene una Compañía en especifico
    ''' </summary>
    ''' <param name="code">Codigo de la Compañía</param>
    ''' <returns>Compañia</returns>
    Function GetCompany(ByVal nit As String, Optional desatach As Boolean = True) As Company

    ''' <summary>
    ''' Obtiene una compañia por id
    ''' </summary>
    ''' <param name="id">id de la compañia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCompanyById(ByVal companyId As Integer) As Company

End Interface
