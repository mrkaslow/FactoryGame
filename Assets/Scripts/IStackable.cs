using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStackable {
    void AddToStack(Product product);
    void RemoveFromStack();
}
