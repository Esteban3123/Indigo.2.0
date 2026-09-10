
'***********************************************************************
' Assembly         : Presentacion.Common
' Author           : Luis Felipe Pantoja Cerquera
' Created          : 17-08-2017
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************


Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.Base
Imports Presentation.Controls

Public Interface IPatientDeparture
    Inherits IcrudBase

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl
    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' propiedad para establcer el datasource de centros de atencion de crystal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CareCenterXPO As XPInstantFeedbackSource
    ''' <summary>
    ''' Listado de centros de atención cargados con store procedure
    ''' </summary>
    ''' <returns></returns>
    Property CareCenterList As List(Of Domain.Entities.SP_ListCareCenterHis_Result)
    ''' <summary>
    ''' id del centro de atencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CareCenterCode As String


    ''' <summary>
    ''' Datasource unidades funcionales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DataSourceUnitFunctional As XPCollection(Of ViewUnitFunctionalHis)

    Property DataSourceUnitFunctionalList As List(Of Domain.Entities.SP_ListFunctionalUnitHis_Result)

    ''' <summary>
    ''' Datasource Pacientes Egresados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PatientDepartureXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que modifica el texto el popup de unidades funcionales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UnitFunctional As String

End Interface
