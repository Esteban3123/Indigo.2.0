'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Daniel Eduardo Arévalo
' Created          : 27-10-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Presentation.Base
Imports DevExpress.Xpo

#End Region

''' <summary>
''' esta interfaz contiene las propiedades y metodos que va implementar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface ISupplierMaintenance

    Inherits IcrudBase

#Region "Propiedades"
    ''' <summary>
    ''' Esta propiedad contiene el codigo del fabricante
    ''' </summary>
    Property CodeSupplier As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre del fabricante
    ''' </summary>
    Property NameSupplier As String
    ''' <summary>
    ''' Esta propiedad contiene el Apellido del fabricante
    ''' </summary>
    Property LastNameSupplier As String
    ''' <summary>
    ''' Esta propiedad contiene el codigo CMMS
    ''' </summary>
    Property CodeCMMS As String
    ''' <summary>
    ''' propiedad que contiene el sitio web del fabricante
    ''' </summary>
    Property WebSiteSupplier As String
    ''' <summary>
    ''' propiedad que contiene el estado
    ''' </summary>
    Property StateSupplier As Boolean
   
    ''' <summary>
    ''' Propiedad que obtiene o establece los dias de plazo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TimeLimitDays As Integer
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object
    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.MaintenanceSequence
    ''' <summary>
    ''' Esta propiedad contiene la ciudad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdCity As Integer
    ''' <summary>
    ''' Propiedad que contiene el listado de ciudades xpo
    ''' </summary>
    Property CitiesXpo As XPInstantFeedbackSource

   
#End Region

End Interface