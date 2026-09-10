Imports Domain.Entities
Imports Domain.Base
Public Interface IResponsibleRepository

    Inherits IRepository(Of Responsible)

    ''' <summary>
    ''' Lists the Responsible all.
    ''' </summary>
    ''' <returns></returns>
    Function ListResponsibleAll() As List(Of ResponsibleAll)
    ''' <summary>
    ''' consulta un Responsible especifico
    ''' </summary>
    ''' <param name="codeResponsible">el codigo del Responsible</param>
    ''' <returns></returns>
    Function GetResponsible(ByVal codeResponsible As String) As Responsible
    ''' <summary>
    ''' consulta un Responsible especifico
    ''' </summary>
    ''' <param name="codeResponsible">el codigo ERP del Responsible</param>
    ''' <returns></returns>
    Function GetResponsibleByCodeERP(codeResponsible As String) As Responsible

    ''' <summary>
    ''' Consulta el ID del responsable
    ''' </summary>
    ''' <param name="codeResponsible"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetIdResponsible(codeResponsible As String) As Integer


    ''' <summary>
    ''' Consulta el ID del concepto
    ''' </summary>
    ''' <param name="codeConcept"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetIdConcepto(codeConcept As String) As Integer

    ''' <summary>
    ''' funcion para validar que no se repita codigo de usuarios
    ''' </summary>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListValidateResponsibleByCodeUSer(codeUser As String) As List(Of Responsible)

End Interface
