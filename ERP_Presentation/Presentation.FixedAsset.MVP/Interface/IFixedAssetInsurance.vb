'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Cardozo
' Created          : 04-08-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Presentation.Base
#End Region

''' <summary>
''' esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface IFixedAssetInsurance
    Inherits IcrudBase

#Region "Propiedades"
    ''' <summary>
    ''' Esta propiedad contiene el codigo de la aseguradora
    ''' </summary>
    Property CodeInsurance As String
    ''' <summary>
    ''' Esta propiedad contiene el codigo de la aseguradora
    ''' </summary>
    Property IdThirdPartyInsurance As Integer
    ''' <summary>
    ''' Esta propiedad contiene el nombre de la aseguradora
    ''' </summary>
    Property NameInsurance As String
    ''' <summary>
    ''' propiedad que contiene el sitio web de la aseguradora
    ''' </summary>
    Property WebSiteInsurance As String
    ''' <summary>
    ''' propiedad que contiene el id de la ciudad
    ''' </summary>
    Property CityInsurance As String
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' Propiedad que carga los departamentos
    ''' </summary>
    WriteOnly Property DepartmentDataSource As List(Of Domain.Entities.Department)
    ''' <summary>
    ''' Propiedad que carga las ciudades
    ''' </summary>
    WriteOnly Property CityDataSource As List(Of Domain.Entities.City)
    ''' <summary>
    ''' Propiedad con el valor del estado
    ''' </summary>
    Property StateInsurance As Boolean

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.FixedAssetSequence


#End Region

End Interface