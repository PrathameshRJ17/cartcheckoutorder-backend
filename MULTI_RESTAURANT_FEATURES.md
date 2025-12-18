# Multi-Restaurant Cart and Checkout System

## Overview
Enhanced the food ordering system to support multiple restaurants in cart and checkout, allowing customers to order from different restaurants simultaneously while maintaining restaurant information throughout the process.

## Key Features Implemented

### 1. Restaurant Management System
- **Complete CRUD operations** for restaurants
- **Role-based access control** (Admin, RestaurantOwner, Customer)
- **Restaurant information** includes cuisine, rating, delivery time, fees

### 2. Multi-Restaurant Cart Support
- **Removed single restaurant restriction** - customers can add items from multiple restaurants
- **Restaurant grouping** in cart response shows items organized by restaurant
- **Restaurant details** displayed for each item in cart

### 3. Enhanced Checkout Experience
- **Restaurant information** visible in checkout summary
- **Multiple restaurant orders** supported (creates separate orders per restaurant)
- **Restaurant-specific details** like delivery time and fees shown

## API Endpoints

### Restaurant Information
```
GET /api/restaurants - Get all active restaurants
GET /api/restaurants/{id} - Get restaurant by ID
```

### Menu Items by Restaurant
```
GET /api/menuitems/restaurant/{restaurantId} - Get menu items for specific restaurant
```

### Enhanced Cart (Multi-Restaurant Support)
```
GET /api/cart - Get cart with restaurant grouping
POST /api/cart/items/validate - Validate item addition (now always allows)
POST /api/cart/items - Add item from any restaurant
PUT /api/cart/items/{id} - Update cart item
DELETE /api/cart/items/{id} - Remove cart item
DELETE /api/cart - Clear entire cart
```

## Data Structures

### Restaurant DTO
```json
{
  "restaurantId": 1,
  "name": "Pizza Palace",
  "description": "Authentic Italian cuisine",
  "imageUrl": "https://example.com/pizza-palace.jpg",
  "cuisine": "Italian",
  "rating": 4.5,
  "deliveryTime": 30,
  "minOrderAmount": 200,
  "deliveryFee": 40,
  "isActive": true
}
```

### Multi-Restaurant Cart Response
```json
{
  "restaurants": [
    {
      "restaurantId": 1,
      "restaurantName": "Pizza Palace",
      "restaurantImage": "url",
      "cuisine": "Italian",
      "rating": 4.5,
      "deliveryTime": 30,
      "minOrderAmount": 200,
      "items": [
        {
          "cartItemId": 1,
          "menuItemName": "Margherita Pizza",
          "restaurantId": 1,
          "restaurantName": "Pizza Palace",
          "unitPrice": 299,
          "quantity": 2,
          "lineTotal": 598
        }
      ],
      "restaurantTotal": 598
    },
    {
      "restaurantId": 2,
      "restaurantName": "Burger King",
      "restaurantImage": "url",
      "cuisine": "Fast Food",
      "rating": 4.2,
      "deliveryTime": 25,
      "minOrderAmount": 150,
      "items": [
        {
          "cartItemId": 2,
          "menuItemName": "Whopper Burger",
          "restaurantId": 2,
          "restaurantName": "Burger King",
          "unitPrice": 199,
          "quantity": 1,
          "lineTotal": 199
        }
      ],
      "restaurantTotal": 199
    }
  ],
  "subTotal": 797,
  "deliveryFee": 80,
  "taxAmount": 39.85,
  "totalAmount": 916.85,
  "totalItems": 3
}
```

### Checkout Summary with Multiple Restaurants
```json
{
  "restaurants": [
    {
      "restaurantId": 1,
      "restaurantName": "Pizza Palace",
      "cuisine": "Italian",
      "rating": 4.5,
      "deliveryTime": 30,
      "items": [...],
      "restaurantTotal": 598
    },
    {
      "restaurantId": 2,
      "restaurantName": "Burger King",
      "cuisine": "Fast Food", 
      "rating": 4.2,
      "deliveryTime": 25,
      "items": [...],
      "restaurantTotal": 199
    }
  ],
  "savedAddresses": [...],
  "paymentOptions": [...],
  "subTotal": 797,
  "deliveryFee": 80,
  "taxAmount": 39.85,
  "totalAmount": 916.85,
  "estimatedDeliveryTime": 30
}
```

## Role-Based Access Control

### Customer Role
- Browse all restaurants
- View menu items by restaurant
- Add items from multiple restaurants to cart
- View cart with restaurant grouping
- Checkout with restaurant information

### Restaurant Owner Role
- View restaurant information
- Manage their restaurant's menu items
- View orders for their restaurant

### Admin Role
- View all restaurant information
- System-wide management

## Key Benefits

### 1. Enhanced User Experience
- **Freedom to order** from multiple restaurants
- **Clear restaurant information** throughout the process
- **Restaurant-specific details** like delivery time and fees
- **Organized cart display** grouped by restaurant

### 2. Business Flexibility
- **Multiple revenue streams** from different restaurants
- **Restaurant competition** within the platform
- **Scalable restaurant management** system

### 3. Technical Improvements
- **Modular architecture** with separate restaurant management
- **Clean separation of concerns** between cart, checkout, and restaurant logic
- **Extensible design** for future restaurant-specific features

## Implementation Details

### Cart System Changes
- Removed single restaurant validation
- Enhanced cart response to group items by restaurant
- Added restaurant information to cart items

### Checkout System Changes
- Modified to handle multiple restaurants
- Creates separate orders per restaurant (can be enhanced for multi-restaurant orders)
- Displays restaurant information in checkout summary

### Repository Pattern
- Added RestaurantRepository with full CRUD operations
- Enhanced MenuItemRepository to filter by restaurant
- Maintained clean separation between data access and business logic

### Service Layer
- RestaurantService handles all restaurant business logic
- CartService updated to support multi-restaurant functionality
- CheckoutService enhanced to process multi-restaurant orders

## Future Enhancements
- **Multi-restaurant single order** - combine items from multiple restaurants in one order
- **Restaurant-specific delivery zones** and availability
- **Dynamic delivery fees** based on distance and restaurant
- **Restaurant ratings and reviews** system
- **Restaurant search and filtering** capabilities
- **Restaurant owner dashboard** for order management