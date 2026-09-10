'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Rafael EduarDo Patiño Cabrera
' Created          : 07-04-2015
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

Public Class EditProductLoanMerchandiseDevolutionDetailEventArgs
    Inherits EventArgs

    ''' <summary>
    ''' Retorna un item de tipo designado cuando esta en modo edicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LoanMerchandiseDevolutionDetail As LoanMerchandiseDevolutionDetail

End Class

