Imports System.Runtime.Serialization

Partial Public Class PaymentNotes

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la descripcion del proveedor
    ''' </summary>
    <DataMember()>
    Public Property DescriptionSupplier As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del centro costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DescriptionCostCenter As String

    ''' <summary>
    ''' Obtiene o establece el tipo de comprobante que si va lleno es porque
    ''' lo hicieron desde inventarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property JournalVoucherId As Integer?

    ''' <summary>
    ''' Agrega detalles que se contabilizarán en otros libros que no sean homologables
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property PaymentsNoteDetailOthersNotHomologatedBooks As New Dictionary(Of Integer, List(Of PaymentsNoteDetails))()

    ''' <summary>
    ''' Obtiene o establece la descripcion de la cuenta por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DescriptionAccountPayable As String

    ''' <summary>
    ''' Obtiene o establece si la nota proviene del formulario nativo de Notas Débito Crédito de CXP
    ''' Por defecto viene en Falso
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property IsNative As Boolean = False


#End Region

#Region "Methods"

    Function ValidateIfBalanced() As Tuple(Of Boolean, String)
        Dim DebitValue As Decimal = 0
		Dim CreditValue As Decimal = 0

		' Función para filtrar ítems válidos
		Dim IsValidItem = Function(item) item.ChangeTracker.State <> Base.Entities.ObjectState.Deleted

		If Me.PaymentNotesAccountPayableAdvance IsNot Nothing AndAlso Me.PaymentNotesAccountPayableAdvance.Count > 0 Then
			Dim validItems = Me.PaymentNotesAccountPayableAdvance.Where(IsValidItem)
			Dim value = validItems.Sum(Function(d) If(d.AccountPayableShareId Is Nothing, d.AdjusmentValue, d.AdjustmentValueShare))
			If Me.Nature = 1 Then
				DebitValue = DebitValue + value
			Else
				CreditValue = CreditValue + value
			End If
        End If

		If Me.PaymentsNoteShares IsNot Nothing AndAlso Me.PaymentsNoteShares.Count > 0 Then
			Dim validItems = Me.PaymentsNoteShares.Where(IsValidItem)
			DebitValue = DebitValue + validItems.Where(Function(d) d.Nature = 1).Sum(Function(d) d.Value)
			CreditValue = CreditValue + validItems.Where(Function(d) d.Nature <> 1).Sum(Function(d) d.Value)
		End If

		If Me.PaymentsNoteDetails IsNot Nothing AndAlso Me.PaymentsNoteDetails.Count > 0 Then
			Dim validItems = Me.PaymentsNoteDetails.Where(IsValidItem)
			DebitValue = DebitValue + validItems.Where(Function(d) d.Nature = 1).Sum(Function(d) If(d.TotalConceptValue IsNot Nothing, d.TotalConceptValue, d.Value))
			CreditValue = CreditValue + validItems.Where(Function(d) d.Nature <> 1).Sum(Function(d) If(d.TotalConceptValue IsNot Nothing, d.TotalConceptValue, d.Value))
		End If

        If DebitValue <> CreditValue Then
            Return New Tuple(Of Boolean, String)(False, String.Format("La nota se encuentra desbalanceada (Debitos: {0} - Creditos: {1})", DebitValue.ToString("0.####"), CreditValue.ToString("0.####")))
        End If

        If DebitValue = 0 Then
            Return New Tuple(Of Boolean, String)(False, String.Format("Debe agregar al menos un detalle a la nota."))
        End If

        Return New Tuple(Of Boolean, String)(True, String.Empty)
    End Function

#End Region

End Class
