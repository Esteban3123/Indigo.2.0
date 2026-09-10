'***********************************************************************
' Assembly         : Presentacion.Corporation.MVP
' Author           : Jose Luis Rojas
' Created          : 07-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo

#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface IEmail
    Inherits ICrudBase

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad contiene el id de persona
    ''' </summary>
    Property IdPerson As Integer

    ''' <summary>
    ''' Esta propiedad contiene la dirección de email
    ''' </summary>
    Property Email As String

    ''' <summary>
    ''' Esta propiedad contiene el estado del registro
    ''' </summary>
    Property State As Byte

    ''' <summary>
    ''' Esta propiedad contiene el estado de sincronización del registro
    ''' </summary>
    Property Synchronized As String

    ''' <summary>
    ''' Esta propiedad contiene el tipo de email
    ''' </summary>
    Property Type As Integer
#End Region
#Region "XPO"

    ''' <summary>
    ''' Datasource de las direcciones de correo
    ''' </summary>
    ''' <returns></returns>
    Property EmailXPO As XPInstantFeedbackSource

#End Region
End Interface

