'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 09-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository

#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presentador
''' </summary>
Public Interface IConcept
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Propiedad que contiene el datasource de los concept group
    ''' </summary>
    WriteOnly Property DatasourceConceptGroup As TrackableCollection(Of ConceptGroup)

    ''' <summary>
    ''' Propiedad que contiene la entidad del concepto
    ''' </summary>
    ReadOnly Property ConceptObject As Concept


#End Region

End Interface
