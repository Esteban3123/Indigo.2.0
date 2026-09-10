'***********************************************************************
' Assembly         : Application.Glosas
' Author           : RafaelPatiño
' Created          : 20-06-2013
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Glosas
Imports Infrastructure.CrossCutting.Base

#End Region

Partial Class GlosasService

#Region "ObjectionsReceptionD"

    ''' <summary>
    ''' elimina un item del detalle de una objecion 
    ''' </summary>
    ''' <param name="ObjectionsReceptionD">el detalle de la objecion</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>valor de confirmacion del eliminado</returns>
    Public Function DeleteObjectionsReceptionD(ObjectionsReceptionD As GlosaObjectionsReceptionD, session As SessionValues) As Boolean Implements IGlosasService.DeleteObjectionsReceptionD
        Using ObjectionsReceptionDAdmin As IObjectionsReceptionDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionDAdminService)()
            Return ObjectionsReceptionDAdmin.DeleteObjectionsReceptionD(ObjectionsReceptionD, session.AuditMessageWcf, session.TransactionalContainer)
        End Using
    End Function

    ''' <summary>
    ''' Eliminacion masiva
    ''' </summary>
    ''' <param name="tmpList"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteListObjectionReceptionD(tmpList As List(Of GlosaObjectionsReceptionD), session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasObjectionsReceptionD.DeleteListObjectionReceptionD
        Using ObjectionsReceptionDAdmin As IObjectionsReceptionDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionDAdminService)()
            Return ObjectionsReceptionDAdmin.DeleteListObjectionReceptionD(tmpList, session.AuditMessageWcf, session.TransactionalContainer)
        End Using
    End Function

    ''' <summary>
    ''' lista del detalle de una objecion
    ''' </summary>
    ''' <param name="codeObjectionReceptionC">codigo de la objecion</param>
    ''' <returns>lista del detalle de una objecion </returns>
    Public Function ListAllObjectionsReceptionD(codeObjectionReceptionC As String, session As SessionValues) As List(Of GlosaObjectionsReceptionD) Implements IGlosasService.ListAllObjectionsReceptionD
        Using ObjectionsReceptionDAdmin As IObjectionsReceptionDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionDAdminService)()
            Return ObjectionsReceptionDAdmin.ListAllObjectionsReceptionD(codeObjectionReceptionC)
        End Using
    End Function

    ''' <summary>
    ''' obtiene el detalle de una recepcion
    ''' </summary>
    ''' <param name="InvoiceNumber">codigo de factura</param>
    ''' <param name="GlosaObjectionsReceptionCId">codigo de Cabecera</param>
    ''' <returns>un objecto detalle de oficio</returns>
    Public Function getObjectionReceptionD(InvoiceNumber As String, GlosaObjectionsReceptionCId As String, session As SessionValues) As GlosaObjectionsReceptionD Implements IGlosasService.getObjectionReceptionD
        Using ObjectionsReceptionDAdmin As IObjectionsReceptionDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionDAdminService)()
            Return ObjectionsReceptionDAdmin.getObjectionReceptionD(InvoiceNumber, GlosaObjectionsReceptionCId)
        End Using
    End Function


    ''' <summary>
    ''' obtiene una lista de detalles de una recepcion
    ''' </summary>
    ''' <param name="GlosaObjectionsReceptionCId">codigo de la recpcion</param>
    ''' <returns>una lista detalle de oficio</returns>
    Public Function ListObjectionReceptionD(GlosaObjectionsReceptionCId As String, session As SessionValues) As List(Of GlosaObjectionsReceptionD) Implements IGlosasService.ListObjectionReceptionD
        Using ObjectionsReceptionDAdmin As IObjectionsReceptionDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionDAdminService)()
            Return ObjectionsReceptionDAdmin.ListObjectionReceptionD(GlosaObjectionsReceptionCId)
        End Using
    End Function

    ''' <summary>
    ''' obtiene una lista de detalles, cargando la cartera glosada, detalles quirugicos
    ''' </summary>
    ''' <param name="GlosaObjectionsReceptionDId">codigo del detalle a buscar</param>
    ''' <returns>una lista detalle de oficio</returns>
    Public Function GetListObjectionReceptionD(GlosaObjectionsReceptionDId As String, session As SessionValues) As List(Of GlosaObjectionsReceptionD) Implements IGlosasService.GetListObjectionReceptionD
        Using ObjectionsReceptionDAdmin As IObjectionsReceptionDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionDAdminService)()
            Return ObjectionsReceptionDAdmin.GetListObjectionReceptionD(GlosaObjectionsReceptionDId)
        End Using
    End Function

    ''' <summary>
    ''' obtiene una lista de detalles confirmados de una recepcion
    ''' </summary>
    ''' <param name="Nit">Nit de la objeción</param>
    ''' <returns>una lista detalle de oficio</returns>
    Public Function ListConfirmObjectionReceptionD(Nit As String, session As SessionValues) As List(Of GlosaObjectionsReceptionD) Implements IGlosasService.ListConfirmObjectionReceptionD
        Using ObjectionsReceptionDAdmin As IObjectionsReceptionDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionDAdminService)()
            Return ObjectionsReceptionDAdmin.ListConfirmObjectionReceptionD(Nit)
        End Using
    End Function

    ''' <summary>
    '''  Funcion Para Actualizar el estado de una factura
    ''' </summary>
    ''' <param name="ObjectionsReceptionDId">Id factura</param>
    ''' <param name="IdSecuense">secuencia numerica para los doc. de reclasificacion</param>
    ''' <param name="Nit">tercero</param>
    ''' <param name="RadicateConsecutive">numero radicado</param>
    ''' <param name="valueGlosa">valor glosa y/o reiteracion</param>
    ''' <param name="session">variable de session</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmObjectionReceptionD(ObjectionsReceptionDId As Integer, ByVal IdSecuense As Integer, ByVal Nit As String, ByVal RadicateConsecutive As String, ByVal valueGlosa As Decimal, session As SessionValues) As ActionResult Implements IGlosasObjectionsReceptionD.ConfirmObjectionReceptionD
        Using ObjectionsReceptionDAdmin As IObjectionsReceptionDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionDAdminService)()
            Return ObjectionsReceptionDAdmin.ConfirmObjectionReceptionD(ObjectionsReceptionDId, IdSecuense, Nit, RadicateConsecutive, valueGlosa, session)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una lista de detalles confirmados de una recepcion segun un responsable
    ''' </summary>
    ''' <param name="CodeResponsible">Id Responsable</param>
    ''' <returns>una lista detalle de oficio</returns>
    Public Function ListObjectionReceptionDByResponsable(CodeResponsible As String, session As SessionValues, ByVal _IdIOperatingUnit As Integer) As List(Of GlosaObjectionsReceptionD) Implements IGlosasObjectionsReceptionD.ListObjectionReceptionDByResponsable
        Using ObjectionsReceptionDAdmin As IObjectionsReceptionDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionDAdminService)()
            Using timeParameterAdmin As ITimeParametersAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITimeParametersAdminService)()
                Return ObjectionsReceptionDAdmin.ListObjectionReceptionDByResponsable(CodeResponsible, timeParameterAdmin, _IdIOperatingUnit)
            End Using
        End Using
    End Function

    ''' <summary>
    ''' Funcion Para Actualizar un Objection receptionD
    ''' </summary>
    ''' <param name="ObjectionsReceptionD">objeto detalle de oficio</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns></returns>
    Function SaveObjectionReceptionD(ObjectionsReceptionD As GlosaObjectionsReceptionD, session As SessionValues) As ActionResult Implements IGlosasObjectionsReceptionD.SaveObjectionReceptionD
        Using ObjectionsReceptionDAdmin As IObjectionsReceptionDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionDAdminService)()
            Return ObjectionsReceptionDAdmin.SaveObjectionReceptionD(ObjectionsReceptionD, session.AuditMessageWcf)
        End Using
    End Function


    ''' <summary>
    ''' Funcion para cargar saldo de factura ERP
    ''' </summary>
    ''' <param name="ObjD"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadBalanceInvoice(ObjD As GlosaObjectionsReceptionD, session As SessionValues) As Decimal Implements IGlosasObjectionsReceptionD.LoadBalanceInvoice
        Using ObjectionsReceptionDAdmin As IObjectionsReceptionDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionDAdminService)()
            Return ObjectionsReceptionDAdmin.LoadBalanceInvoice(ObjD, session)
        End Using
    End Function

    ''' <summary>
    ''' Asignación de responsable de radicacion respuesta EAPB
    ''' </summary>
    ''' <param name="listObjetionReceptionDetail"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function GlosaObjetionReceptionDetailAssignRadicateResponsible(listObjetionReceptionDetail As List(Of GlosaObjectionsReceptionD), session As SessionValues) As ActionResult(Of List(Of GlosaObjectionsReceptionD)) Implements IGlosasService.GlosaObjetionReceptionDetailAssignRadicateResponsible
        Using ObjectionsReceptionDAdmin As IObjectionsReceptionDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionDAdminService)()
            Return ObjectionsReceptionDAdmin.GlosaObjetionReceptionDetailAssignRadicateResponsible(listObjetionReceptionDetail)
        End Using
    End Function

    ''' <summary>
    ''' Radicación de la respuesta EAPB
    ''' </summary>
    ''' <param name="listObjetionReceptionDetail"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function GlosaObjetionReceptionDetailRadicate(listObjetionReceptionDetail As List(Of GlosaObjectionsReceptionD), session As SessionValues) As ActionResult(Of List(Of GlosaObjectionsReceptionD)) Implements IGlosasService.GlosaObjetionReceptionDetailRadicate
        Using ObjectionsReceptionDAdmin As IObjectionsReceptionDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionDAdminService)()
            Return ObjectionsReceptionDAdmin.GlosaObjetionReceptionDetailRadicate(listObjetionReceptionDetail)
        End Using
    End Function

#End Region

#Region "import data excel"

    Public Function ValidateExcelData(dtSet As DataSet, session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasObjectionsReceptionD.ValidateExcelData
        Using ObjectionsReceptionDAdmin As IObjectionsReceptionDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionDAdminService)()
            Return ObjectionsReceptionDAdmin.ValidateExcelData(dtSet, session)
        End Using
    End Function

#End Region

End Class
