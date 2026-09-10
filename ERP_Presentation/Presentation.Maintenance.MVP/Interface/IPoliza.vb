'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Cardozo
' Created          : 10-08-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Presentation.Base
Imports Presentation.Controls
#End Region

''' <summary>
''' esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface IPoliza

    Inherits IcrudBase

#Region "Propiedades"
    ''' <summary>
    ''' Esta propiedad contiene el codigo de la poliza
    ''' </summary>
    Property CodePoliza As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre de la poliza
    ''' </summary>
    Property NamePoliza As String
    ''' <summary>
    ''' Propiedad que contiene el id de la aseguradora
    ''' </summary>
    Property IdInsurance As Integer
    ''' <summary>
    ''' propiedad que contiene la fecha inicial de la poliza
    ''' </summary>
    Property InitialDate As Date
    ''' <summary>
    ''' propiedad que contiene la fecha final de la poliza
    ''' </summary>
    Property EndDate As Date
    ''' <summary>
    ''' propiedad que contiene el tipo de poliza
    ''' </summary>
    Property IdPolizaType As Integer
    ''' <summary>
    ''' propiedad que contiene el objecto de la poliza
    ''' </summary>
    Property Objects As String
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' Propiedad que carga las aseguradoras
    ''' </summary>
    WriteOnly Property InsuranceDataSource As List(Of Domain.Maintenance.Entities.Insurance)
    ''' <summary>
    ''' Propiedad que carga los tipos de poliza
    ''' </summary>
    WriteOnly Property PolizaTypeDataSource As List(Of Domain.Maintenance.Entities.PolizaType)
    ''' <summary>
    ''' Propiedad estado de la poliza
    ''' </summary>
    Property StatePoliza As Boolean

    ReadOnly Property MyTag As Object
    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.MaintenanceSequence

    ReadOnly Property MyLayoutControl As IndigoLayoutControl

#End Region

End Interface