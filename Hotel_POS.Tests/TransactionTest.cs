using Xunit;
using Hotel_POS.ViewModel;
using Hotel_POS.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Hotel_POS.Tests
{
    public class TransactionTest
    {
        [Fact]

        public void CatClick_UpdatesCurrentSelection()
        {
            // Arrange
            var viewModel = new CashierWindowVM();
            var category = new Category("TestCategory");

            // Act
            viewModel.CatClick(category);

            // Assert
            Assert.Equal(viewModel.currentSelection, viewModel.items.Where(i => i.itemCategory == category.Name));
        }

        [Fact]
        public void TotalItems_InitiallyZero()
        {
            // Arrange
            var viewModel = new CashierWindowVM();

            // Assert
            Assert.Equal(0, viewModel.TotalItems);
        }

        [Fact]
        public void ItemClicked_AddsNewItemToBillRowItems()
        {
            // Arrange
            var viewModel = new CashierWindowVM();
            var item = new Item { itemId = 1, itemName = "Item 1", itemPrice = 10 };

            // Act
            viewModel.ItemClicked(item);

            // Assert
            Assert.Single(viewModel.BillRowItems);
            Assert.Equal(1, viewModel.BillRowItems[0].Qty);
            Assert.Equal(10, viewModel.GrandTotal);
        }

        [Fact]
        public void GeneratePrint_CreatesFlowDocument()
        {
            // Arrange
            var viewModel = new CashierWindowVM();

            // Act
            var flowDocument = viewModel.CreateFlowDocument();

            // Assert
            Assert.NotNull(flowDocument);
            Assert.NotEmpty(flowDocument.Blocks);
        }

        [Fact]
        public void Increment_IncreasesQtyAndGrandTotal()
        {
            // Arrange
            var viewModel = new CashierWindowVM();
            var item = new Item { itemPrice = 10 }; // Create a sample item
            var billRowItem = new BillRowItem("1", 1, item, 1, 10, "Add comment");
            viewModel.BillRowItems.Add(billRowItem);

            // Store the initial values for comparison
            var initialQty = billRowItem.Qty;
            var initialGrandTotal = viewModel.GrandTotal;

            // Act
            viewModel.Increment(billRowItem);

            // Assert
            Assert.Equal(initialQty + 1, billRowItem.Qty);
            Assert.Equal(initialGrandTotal + item.itemPrice, viewModel.GrandTotal);
        }

        [Fact]
        public void DeleteRow_RemovesItemFromBillRowItems()
        {
            // Arrange
            var viewModel = new CashierWindowVM();
            var item = new Item { itemId = 1, itemName = "Item 1", itemPrice = 10 };
            var billRowItem = new BillRowItem("1", 1, item, 1, 10, "Add comment");
            viewModel.BillRowItems.Add(billRowItem);

            // Act
            // Store the initial values for comparison
            var initialGrandTotal = viewModel.GrandTotal;
            viewModel.DeleteRow(billRowItem);

            // Assert
            Assert.Empty(viewModel.BillRowItems);
            Assert.Equal(initialGrandTotal - (billRowItem.Qty * billRowItem.Item.itemPrice), viewModel.GrandTotal);
        }
    }
}