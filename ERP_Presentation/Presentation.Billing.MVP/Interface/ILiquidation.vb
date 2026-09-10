'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Juan F. Tamayo
' Created          : 2014-11-10
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports Domain.Entities

#End Region

Public Interface ILiquidation

#Region "Members"

    ''' <summary>
    ''' Obtiene una lista de pares Id/FolioOrder de los folios mostrados
    ''' </summary>
    ''' <returns>Lista de folios</returns>
    ReadOnly Property ListFoliosIdOrder As List(Of Object)

    ''' <summary>
    ''' Obtiene o asigna la lista de unidades operativas
    ''' </summary>
    ''' <value>Lista de unidades operativa</value>
    ''' <returns>La lista de unidades operativas</returns>
    Property ListOperatingUnit As List(Of Domain.Entities.OperatingUnit)

    ''' <summary>
    ''' Obtiene la unidad operativa seleccionada
    ''' </summary>
    ''' <returns>Unidad operativa seleccionada</returns>
    ReadOnly Property OperatingUnitSelected As Domain.Entities.OperatingUnit

    ''' <summary>
    ''' Obtiene el Id de la unidad operativa seleccionada
    ''' </summary>
    ''' <returns>Id de la unidad operativa seleccionad</returns>
    Property IdOperatingUnitSelected As Int32

    ''' <summary>
    ''' Obtiene o asigna la lista de autorizaciones de facturación
    ''' asignadas al usuario
    ''' </summary>
    ''' <value>Lista de autorizaciones de facturación</value>
    ''' <returns>Lamlista de autorizaciones de facturación</returns>
    Property ListBillingAuthorization As List(Of BillingAuthorization)

    ''' <summary>
    ''' Obtiene la autorización seleccionada
    ''' </summary>
    ''' <returns>La autorización seleccionada</returns>
    ReadOnly Property BillingAuthorizationSelected As BillingAuthorization

    ''' <summary>
    ''' Obtiene el Id de la autorización seleccionada
    ''' </summary>
    ''' <returns>El Id de la autorización seleccionada</returns>
    Property IdBillingAuthorizationSelected As Int32

    ''' <summary>
    ''' Obtiene o asigna el tag del frontal
    ''' </summary>
    ''' <value>Tag del frontal</value>
    ''' <returns>El tag del frontal</returns>
    ReadOnly Property MyTag As String

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si
    ''' se encuentra ejecutando un proceso
    ''' </summary>
    ''' <value>Valor que indica si el frontal esta ocupado</value>
    ''' <returns>El valor que indica si el frontal esta ocupado</returns>
    Property IsBusy As Boolean

    ''' <summary>
    ''' Obtiene o asigna los permisos que el usuario tiene asignados en éste formulario
    ''' </summary>
    ''' <value>Diccionario de permisos del usuario</value>
    ''' <returns>El diccionario de permisos del usuario</returns>
    Property PermissionsForm As Dictionary(Of Integer, String)


    ReadOnly Property TxtPatientCode As Object
    ReadOnly Property Name As String
    ReadOnly Property TxtPatientName As Object

    ''' <summary>
    ''' Bandera que establece si el sistema es impuesto incluido o no
    ''' </summary>
    Property FlagTaxInclude As Boolean


#End Region

End Interface
