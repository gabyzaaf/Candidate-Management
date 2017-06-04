//
//  ViewControllerTableViewCell.swift
//  HR
//
//  Created by zaafrani Gabriel on 03/06/2017.
//  Copyright © 2017 zaafrani Gabriel. All rights reserved.
//

import UIKit

class ViewControllerTableViewCell: UITableViewCell {

    
    @IBOutlet weak var Lname: UILabel!
    @IBOutlet weak var Lfirstname: UILabel!
    @IBOutlet weak var Lemail: UILabel!
    
    override func awakeFromNib() {
        super.awakeFromNib()
        // Initialization code
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }

}
