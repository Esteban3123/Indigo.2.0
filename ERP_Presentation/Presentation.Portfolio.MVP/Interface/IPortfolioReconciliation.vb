#Region "Librerias Importadas"

Imports Presentation.Base

#End Region

''' <summary>
''' esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface IPortfolioReconciliation
    Inherits ICrudBase

#Region "Propiedades"

    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface