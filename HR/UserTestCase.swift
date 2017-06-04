//
//  UserTestCase.swift
//  HR
//
//  Created by zaafrani Gabriel on 31/05/2017.
//  Copyright © 2017 zaafrani Gabriel. All rights reserved.
//

import XCTest

class UserTestCase: XCTestCase {
    
    override func setUp() {
        super.setUp()
        // Put setup code here. This method is called before the invocation of each test method in the class.
    }
    
    override func tearDown() {
        // Put teardown code here. This method is called after the invocation of each test method in the class.
        super.tearDown()
    }
    
    func testExample() {
        var nb:Int = 2
        XCTAssertTrue(nb == 2)
    }
    
    func testPerformanceExample() {
        // This is an example of a performance test case.
        self.measure {
            // Put the code you want to measure the time of here.
        }
    }
    
}
