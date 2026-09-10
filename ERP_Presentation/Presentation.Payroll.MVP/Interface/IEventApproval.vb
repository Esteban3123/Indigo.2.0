'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 25-10-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports Presentation.Base
Imports Domain.Payroll.Entities
#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface IEventApproval
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Propiedad solo escritura que contiene el datasource de los terceros
    ''' </summary>
    WriteOnly Property EventScheduleDetailDataSource As List(Of ScheduleDetail)


    WriteOnly Property GroupDatasource As DevExpress.Xpo.XPInstantFeedbackSource


#End Region

End Interface

