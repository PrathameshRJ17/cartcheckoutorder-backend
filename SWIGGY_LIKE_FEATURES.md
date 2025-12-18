# Swiggy/Zomato-like Cart and Checkout System

## Overview
This enhanced food ordering system now includes Swiggy/Zomato-like features for cart management and checkout process with role-based access for Users (Customers), Admins, and Restaurant Owners.

## Key Features Implemented

### 1. Enhanced Cart System
- **Restaurant-grouped Cart**: Items are grouped by restaurant in the cart
- **Single Restaurant Rule**: Users can only order from one restaurant at a time
- **Cart Validation**: Warns users when adding items from different restaurants
- **Detailed Item Information**: Includes images, vegetarian indicators, categories
- **Pricing Breakdown**: Subtotal, delivery fee, taxes, and total amount

### 2. Enhanced Checkout Process
- **Address Management**: Multiple saved addresses with default selection
- **Address Types**: Home, Work, Other address categories
- **Detailed Address Fields**: Complete address with landmark, city, state, pincode
- **Payment Options**: Multiple payment methods (COD, Cards, UPI, Net Banking, Wallet)
- **Promo Codes**: Support for discount codes with validation
- **Delivery Instructions**: Custom delivery notes
- **Contactless Delivery**: Options for contactless delivery

### 3. Role-Based Access Control
- **Customer Role**: Can browse, add to cart, and place orders
- **Admin Role**: Can manage system-wide operations
- **Restaurant Owner Role**: Can manage their restaurant's menu and orders

## API Endpoints

### Cart Management
```
GET /api/cart - Get user's cart with restaurant grouping
POST /api/cart/items/validate - Validate adding item (different restaurant check)
POST /api/cart/items - Add item to cart
PUT /api/cart/items/{id} - Update cart item quantity
DELETE /api/cart/items/{id} - Remove item from cart
DELETE /api/cart - Clear entire cart
```

### Checkout Process
```
GET /api/checkout/summary - Get checkout summary with addresses and payment options
POST /api/checkout/apply-promo - Apply promo code for discount
POST /api/checkout - Place order with address and payment details
```

## Enhanced Data Models

### Cart Response Structure
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
          "imageUrl": "url",
          "unitPrice": 299,
          "quantity": 2,
          "lineTotal": 598,
          "isVeg": true,
          "category": "Pizza"
        }
      ],
      "restaurantTotal": 598
    }
  ],
  "subTotal": 598,
  "deliveryFee": 40,
  "taxAmount": 29.90,
  "totalAmount": 667.90,
  "totalItems": 2
}
```

### Checkout Summary Structure
```json
{
  "restaurants": [...],
  "savedAddresses": [
    {
      "addressId": 1,
      "addressLine": "123 Main Street",
      "landmark": "Near City Mall",
      "city": "Mumbai",
      "state": "Maharashtra",
      "pinCode": "400001",
      "type": "Home",
      "isDefault": true,
      "contactNumber": "9876543210"
    }
  ],
  "paymentOptions": [
    {
      "method": "CashOnDelivery",
      "displayName": "Cash on Delivery",
      "description": "Pay when your order arrives",
      "isAvailable": true,
      "icon": "💵"
    }
  ],
  "subTotal": 598,
  "deliveryFee": 40,
  "taxAmount": 29.90,
  "totalAmount": 667.90,
  "estimatedDeliveryTime": 30
}
```

## Enhanced Features

### 1. Cart Validation
- Prevents mixing items from different restaurants
- Shows confirmation dialog when switching restaurants
- Displays current restaurant and item count

### 2. Address Management
- Complete address fields (line, landmark, city, state, pincode)
- Address types (Home, Work, Other)
- Contact numbers for delivery
- Default address selection

### 3. Payment Integration Ready
- Multiple payment method support
- Payment status tracking
- Transaction ID storage for online payments

### 4. Promo Code System
- Built-in promo codes (FIRST10, SAVE50, WELCOME20)
- Percentage and fixed amount discounts
- Maximum discount limits

### 5. Order Enhancement
- Detailed pricing breakdown in orders
- Delivery instructions storage
- Contactless delivery options
- Promo code application tracking

## Database Schema Updates

### Enhanced Entities
- **MenuItem**: Added ImageUrl, IsVeg, Category fields
- **Restaurant**: Added ImageUrl, Cuisine, Rating, DeliveryTime, MinOrderAmount, DeliveryFee
- **Address**: Added Landmark, City, State, PinCode, Type, ContactNumber fields
- **Order**: Added SubTotal, DeliveryFee, TaxAmount, DiscountAmount, DeliveryInstructions, PromoCode, ContactlessDelivery
- **Payment**: Enhanced with multiple payment methods

### New Enums
- **AddressType**: Home, Work, Other
- **ContactlessDelivery**: No, LeaveAtDoor, HandToMe
- **PaymentMethod**: CashOnDelivery, CreditCard, DebitCard, UPI, NetBanking, Wallet

## Usage Examples

### Adding Items to Cart
1. Call `/api/cart/items/validate` to check if item can be added
2. If different restaurant, show confirmation dialog
3. Call `/api/cart/items` to add item to cart

### Checkout Process
1. Call `/api/checkout/summary` to get addresses and payment options
2. User selects address and payment method
3. Optionally apply promo code with `/api/checkout/apply-promo`
4. Call `/api/checkout` to place order

## Security Features
- JWT-based authentication
- Role-based authorization
- User-specific cart and order access
- Secure payment method handling

## Future Enhancements
- Real-time order tracking
- Push notifications
- Advanced promo code management
- Restaurant-specific delivery zones
- Dynamic pricing based on demand
- Integration with payment gateways
- Order rating and review system