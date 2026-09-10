'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/11/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
#End Region

Public Class AddFeesEventArgs
    Inherits EventArgs

    ''' <summary>
    ''' Propiedad que contiene el objeto de la vista
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ServiceOrderDetailSurgical As ServiceOrderDetailSurgical

    ''' <summary>
    ''' Representa a la entidad xpo Qx
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ViewListSurgicalAndPackage As Infrastructure.Data.Xpo.BillingRepository.ViewListSurgicalAndPackageXpo

    ''' <summary>
    ''' Representa a la entidad xpo NoQx
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ViewListNoSurgical As Infrastructure.Data.Xpo.BillingRepository.ViewListNoSurgical

    ''' <summary>
    ''' Representa a la entidad en Entity NoQx
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DomainViewListNoSurgical As Domain.Entities.ViewListNoSurgical

End Class
