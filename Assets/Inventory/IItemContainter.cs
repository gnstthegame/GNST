﻿
public interface IItemContainter {
    Item RemoveItem(string itemID);
    bool RemoveItem(Item item);
    int ItemCount(string itemID);
    bool AddItem(Item item);
    bool IsFull();
}
