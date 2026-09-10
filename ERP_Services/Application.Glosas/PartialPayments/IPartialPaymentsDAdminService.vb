'***********************************************************************
' Assembly         : Application.Glosas
' Author           : RafaelPatiño
' Created          : 09-04-2013
'
' Last Modified By : RafaelPatiño
' Last Modified On : 2013-09-04
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IPartialPaymentsDAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Funcion para eliminacion de facturas en pagos parciales
    ''' </summary>
    ''' <param name="tmpList">lista de item a eliminar</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeletePartialPaymentsD(ByVal tmpList As List(Of PartialPaymentsD), audit As AuditMessage) As ActionResult


End Interface
