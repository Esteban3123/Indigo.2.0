'***********************************************************************
' Assembly         : Application.Glosas
' Author           : JulianCardozo
' Created          : 11-03-2011
'
' Last Modified By : JulianCardozo
' Last Modified On : 2013-04-06
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region
Public Interface IResponsibleAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lists the Responsible all.
    ''' </summary>
    ''' <returns></returns>
    Function ListResponsibleAll(ByVal audit As AuditMessage) As List(Of ResponsibleAll)
    ''' <summary>
    ''' Elimina un Responsiblee
    ''' </summary>
    ''' <param name="Responsible">el Grupo</param>
    ''' <returns></returns>
    Function DeleteResponsible(ByVal Responsible As Responsible, ByVal audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' graba un Responsiblee
    ''' </summary>
    ''' <param name="Responsible">el Responsiblee</param>
    ''' <returns></returns>
    Function SaveResponsible(ByVal Responsible As Responsible, ByVal audit As AuditMessage) As ActionResult(Of Responsible)
    ''' <summary>
    ''' consulta un Responsiblee especifico
    ''' </summary>
    ''' <param name="codeConcept">el codigo del Responsiblee</param>
    ''' <returns></returns>
    Function GetResponsible(ByVal codeConcept As String, ByVal audit As AuditMessage) As Responsible
    ''' <summary>
    ''' consulta un Responsible especifico
    ''' </summary>
    ''' <param name="codeResponsible">el codigo ERP del Responsible</param>
    ''' <returns></returns>
    Function GetResponsibleByCodeERP(codeResponsible As String, ByVal audit As AuditMessage) As Responsible
    ''' <summary>
    ''' Función para listar todos los responsables que 
    ''' puede ser reasignados
    ''' </summary>
    ''' <param name="IdResponsible">Id del Responsable</param>
    ''' <returns>Lista de objetos ResponsibleMovements</returns>
    Function listAllResponsiblesTransfer(IdResponsible As String) As List(Of ResponsibleMovements)




End Interface
